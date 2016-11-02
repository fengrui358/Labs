using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.Plugins.File;
using MvxXamarinFormsApp.Core.Services;
using Xamarin.Forms;

namespace MvxXamarinFormsApp.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        //todo:修改为自己的测试地址，注意将测试dll放在对应的位置
        private const string TestDllDownLoadUrl = "http://192.168.50.158/Downloads/PluginTest.dll";        

        private readonly HttpClient _httpClient = new HttpClient();
        private bool _isLoading;

        private const string PluginDirectoryPath = "Plugins";

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    RaisePropertyChanged();

                    DynamicLoadCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public MvxCommand DynamicLoadCommand => new MvxCommand(DynamicLoadCommandHandler, () => !IsLoading);

        public Action<View> LoadViewAction { get; set; }

        private async void DynamicLoadCommandHandler()
        {
            IsLoading = true;

            var fileStore = Mvx.Resolve<IMvxFileStore>();

            //todo:程序集目录需要搞个子目录来存放，而且要注意不同插件的名字重复
            var dynamicDllPath = "PluginTest.dll";

            try
            {
                if (!VaildDynamicDllIsExist(dynamicDllPath))
                {
                    await Task.Run(async () =>
                    {
                        using (var response = await _httpClient.GetAsync(TestDllDownLoadUrl))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var fileBytes = await response.Content.ReadAsByteArrayAsync();

                                fileStore.WriteFile(dynamicDllPath, fileBytes);
                            }
                        }
                    });
                }

                var assemblyService = Mvx.Resolve<IAssemblyService>();

                assemblyService.LoadFrom(fileStore.NativePath(dynamicDllPath));

                //加载视图
                Dispatcher.RequestMainThreadAction(() =>
                {
                    var view = assemblyService.GetView("测试插件");

                    LoadViewAction?.Invoke(view);
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private bool VaildDynamicDllIsExist(string path)
        {
            var fileStore = Mvx.Resolve<IMvxFileStore>();

            return fileStore.Exists(path);
        }
    }
}
