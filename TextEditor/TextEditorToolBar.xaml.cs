using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for TextEditorToolBar.xaml
    /// </summary>
    public partial class TextEditorToolBar : UserControl
    {
        public TextEditorToolBar()
        {
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            for (double i =8; i<48; i += 2)
            {
                fontSize.Items.Add(i);
            }
        }

        public bool IsSynchronizing { get; private set; }

        public void SynchronizeWith(TextSelection selection)
        {
            IsSynchronizing = true;

            Synchronize<Double>(selection, TextBlock.FontSizeProperty, SetFontSize);
            Synchronize<FontWeight>(selection, TextBlock.FontWeightProperty, SetFontWeight);
            Synchronize<FontStyle>(selection, TextBlock.FontStyleProperty, SetFontSytle);
            Synchronize<TextDecorationCollection>(selection, TextBlock.TextDecorationsProperty, SetTextDecoration);
            Synchronize<FontFamily>(selection, TextBlock.FontFamilyProperty, SetFontFamily);

            IsSynchronizing = false;
        }


        private void Synchronize<T>(TextSelection selection,
            DependencyProperty property, Action<T> methodToCall)
        {
            object value = selection.GetPropertyValue(property);
            if (value != DependencyProperty.UnsetValue) methodToCall((T)value);
        }


        private void SetFontSize(double size)
        {
            fontSize.SelectedValue = size;
        }
        private void SetFontWeight(FontWeight weight)
        {
            boldButton.IsChecked = ((FontWeight)weight == FontWeights.Bold);
        }
        private void SetFontSytle(FontStyle style)
        {
            italicButton.IsChecked = style == FontStyles.Italic;
        }
        private void SetTextDecoration(TextDecorationCollection decoration)
        {
            underlineButton.IsChecked = decoration == TextDecorations.Underline;
        }
        private void SetFontFamily(FontFamily family)
        {
            fonts.SelectedItem = family;
        }
    }
}
