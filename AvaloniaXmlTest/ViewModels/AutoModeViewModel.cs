using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using AvaloniaXmlTest.Domains;
using AvaloniaXmlTest.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using AvaloniaXmlTest.Views;

namespace AvaloniaXmlTest.ViewModels
{
    public class AutoModeViewModel : ViewModelBase
    {
        private DeviceModel? _device;

        private CancellationTokenSource _cancelTokenSource;
        private CancellationToken _token;
        private bool _canStart;
        private FileInfo _fileInfo;

        private string _startBtnText;

        public DeviceModel Device
        {
            get => _device;
            set => this.RaiseAndSetIfChanged(ref _device, value);
        }
        public bool StartProgressBar { get; set; }
        public string StartBtnText
        {
            get => _startBtnText;
            set => this.RaiseAndSetIfChanged(ref _startBtnText, value);
        }
        public string LogText => Log.GetInstance().Print(Log.Modes.Auto);
        public int TextBoxCaretIndex
        {
            get
            {
                if (_device == null)
                    return 0;
                else
                    return LogText.Length;
            }
        }

        public FileInfo FileInfo
        {
            get => _fileInfo;
            set => this.RaiseAndSetIfChanged(ref _fileInfo, value);
        }

        public ReactiveCommand<Unit, Task> StartTestCommand { get; }
        public ReactiveCommand<Unit, Task> OpenFileCommand =>
            ReactiveCommand.Create(async () =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    openFileDialog.Filters?.Add(new FileDialogFilter() { Name = "XML Files", Extensions = { "xml" } });
                    string[]? result = await openFileDialog.ShowAsync(desktop.MainWindow);
                    if (result != null)
                    {
                        FileInfo = new FileInfo(result[0]);
                    }
                }
            });

        public AutoModeViewModel()
        {
            _canStart = true;
            _device = null;

            MessageBus.Current.Listen<DeviceModel>().Subscribe(Observer.Create<DeviceModel>((device) =>
            {
                Device = device;
                Device.TestCompleted += delegate ()
                {
                    StartBtnText = Properties.Resources.StartBtnText;
                    Log.GetInstance().Write(Log.Modes.Auto, "Тест завершен");
                    _canStart = true;
                    StartProgressBar = false;
                    this.RaisePropertyChanged(nameof(StartProgressBar));
                    this.RaisePropertyChanged(nameof(LogText));
                    this.RaisePropertyChanged(nameof(TextBoxCaretIndex));
                };

                Device.TestStart += delegate ()
                {
                    StartProgressBar = true;
                    Log.GetInstance().Write(Log.Modes.Auto, "Тест запущен");
                    this.RaisePropertyChanged(nameof(StartProgressBar));
                    this.RaisePropertyChanged(nameof(LogText));
                    this.RaisePropertyChanged(nameof(TextBoxCaretIndex));
                };
                Device.TestCommandCompleted += delegate (string answer)
                {
                    Log.GetInstance().Write(Log.Modes.Auto, answer);
                    this.RaisePropertyChanged(nameof(LogText));
                    this.RaisePropertyChanged(nameof(TextBoxCaretIndex));
                };
            }));

            StartBtnText = Properties.Resources.StartBtnText;

            var startButtonEnabled = this.WhenAnyValue(x => x.FileInfo.Name, x => !string.IsNullOrEmpty(x));
            StartTestCommand = ReactiveCommand.Create(async () =>
            {
                if (_canStart)
                {
                    List<RelayModel> _relaysList = XmlParser.GetRelays(FileInfo.FullName);
                    if (_relaysList.Count == 0)
                    {
                        if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                        {
                            await MessageBox.Show(desktop.MainWindow, "XML файл не соответствует", "Ошибка", MessageBox.MessageBoxButtons.Ok);
                            return;
                        }
                    }

                    _canStart = false;
                    _cancelTokenSource = new CancellationTokenSource();
                    _token = _cancelTokenSource.Token;
                    StartBtnText = Properties.Resources.StopBtnText;
                    _device?.StartTestFromFile(_token, _relaysList);
                    Properties.Settings.Default.Save();
                }
                else
                {
                    _canStart = true;
                    _cancelTokenSource.Cancel();
                }
            }, startButtonEnabled);
        }
    }
}
