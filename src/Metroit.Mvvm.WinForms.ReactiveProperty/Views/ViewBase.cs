using Metroit.Mvvm.WinForms.ReactiveProperty.ViewModels;
using System.Windows.Forms;

namespace Metroit.Mvvm.WinForms.ReactiveProperty.Views
{
    /// <summary>
    /// メイン画面の操作を提供します。
    /// </summary>
    public class ViewBase : Form
    {
        /// <summary>
        /// ViewModel を取得します。
        /// </summary>
        protected ViewModelBase ViewModel { get; }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public ViewBase() : base()
        {

        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="viewModel">ViewModel。</param>
        public ViewBase(ViewModelBase viewModel) : base()
        {
            ViewModel = viewModel;

            ViewModel.ExecuteInformationMessage = message =>
            {
                MessageBox.Show(message, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            ViewModel.ExecuteConfirmMessage = (message, buttons) =>
            {
                return MessageBox.Show(message, Text, buttons, MessageBoxIcon.Question);
            };
            ViewModel.ExecuteWarningMessage = message =>
            {
                MessageBox.Show(message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };
            ViewModel.ExecuteErrorMessage = message =>
            {
                MessageBox.Show(message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            };
        }
    }
}
