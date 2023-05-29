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

namespace Wpf_dz3_BestOil
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            comboboxGasType.SelectedIndex = 0;  // тип бензина по умоляанию 1ый из списка
            rdbAmountGas.IsChecked = true;  // радиоБаттон Кол-во по умолчанию включен

            // txtbox для Кол-ва в кафе - по умолчанию только для чтения
            txtboxHotdogAmount.IsReadOnly = true;
            txtboxBurgerAmount.IsReadOnly = true;
            txtboxPotatoAmount.IsReadOnly = true;
            txtboxColaAmount.IsReadOnly = true;

            // заполняем цену в txtbox Цена в Кафе
            txtboxHotdogPrice.Text = cafeHotdogPrice.ToString();
            txtboxBurgerPrice.Text = cafeBurgerPrice.ToString();
            txtboxPotatoPrice.Text = cafePotatoPrice.ToString();
            txtboxColaPrice.Text = cafeColaPrice.ToString();
        }

        // цены для видов бензина
        double gasA95PlusPrice = 50;
        double gasA95Price = 45;
        double gasA92Price = 40;
        double gasDTPrice = 35;
        double gasGasPrice = 30;

        // цены для видов товаров в кафе
        double cafeHotdogPrice = 60;
        double cafeBurgerPrice = 70;
        double cafePotatoPrice = 40;
        double cafeColaPrice = 20;

        // метод обработки события - выбор типа бензина в combobox (Заправка)
        private void comboboxGasType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(comboboxGasType.SelectedIndex)  // в зависимости от выбранного типа бензина
            {                                      // проставится цена этого типа бензина
                case 0:
                    txtboxPriceGas.Text = gasA95PlusPrice.ToString(); 
                    break;
                case 1:
                    txtboxPriceGas.Text = gasA95Price.ToString();
                    break;
                case 2:
                    txtboxPriceGas.Text = gasA92Price.ToString();
                    break;
                case 3:
                    txtboxPriceGas.Text = gasDTPrice.ToString();
                    break;
                case 4:
                    txtboxPriceGas.Text = gasGasPrice.ToString();
                    break;
            }
            txtboxTotalGas.Text = TotalGasStation().ToString();  // меняем итого (Заправка)
        }

        // метод обработки события - нажатия rdb (Заправка)
        private void rdbAmountGas_Checked(object sender, RoutedEventArgs e)
        {
            if(rdbAmountGas.IsChecked == true)  // если нажата rdb Колличество
            {
                txtboxAmountGas.IsReadOnly = false;   // rdb Кол-во - не только для чтения
                txtboxSumGas.IsReadOnly = true;       // rdb Сумма - только для чтения
                txtboxSumGas.Text = 0.ToString();     // txtbox Сумма утсанавливаем 0 по умолчанию
                txtboxAmountGas.Text = 0.ToString();  // txtbox Кол-во утсанавливаем 0 по умолчанию
                grboxToPayGas.Header = "До сплати";   // название блока ИтогоЗаправка - "До сплати",грн
                lbl1.Content = "грн.";
            }              
            else if (rdbSumGas.IsChecked == true)
            {
                txtboxSumGas.IsReadOnly = false;      // rdb Сумма - не только для чтения
                txtboxAmountGas.IsReadOnly = true;    // rdb Кол-во - только для чтения
                txtboxAmountGas.Text = 0.ToString();  // txtbox Кол-во утсанавливаем 0 по умолчанию
                txtboxSumGas.Text = 0.ToString();     // txtbox Сумма утсанавливаем 0 по умолчанию
                grboxToPayGas.Header = "До видачі";   // название блока ИтогоЗаправка - "До видачі",л
                lbl1.Content = "л.";
            }
            txtboxTotalGas.Text = TotalGasStation().ToString();  // меняем итого (Заправка)
        }

        // метод для изменения txtbox Кол-во (Заправка)
        private void txtboxAmountGas_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtboxTotalGas.Text = TotalGasStation().ToString();  // меняем итого (Заправка)
        }

        // метод для изменения txtbox Сумма (Заправка)
        private void txtboxSumGas_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtboxTotalGas.Text = TotalGasStation().ToString();  // меняем итого (Заправка)
        }

        // метод Подсчет стоимости бензина
        private double TotalGasStation()
        {
            double total = 0;
            if (txtboxAmountGas.Text == string.Empty)  // если txtbox Кол-во пустой
                txtboxAmountGas.Text = 0.ToString();   // устанавливаем значение 0
            if (txtboxSumGas.Text == string.Empty)     // если txtbox Сумма пустой
                txtboxSumGas.Text = 0.ToString();      // устанавливаем значение 0
            if (rdbSumGas.IsChecked == true)  // если rdb Сумма включена
            {
                total = double.Parse(txtboxSumGas.Text)/double.Parse(txtboxPriceGas.Text);
            }
            else if(rdbAmountGas.IsChecked == true)  // если rdb Кол-во включена
            {
                total = double.Parse(txtboxAmountGas.Text) * double.Parse(txtboxPriceGas.Text);
            }
            return total;
        }

        // метод нажатия одного из CheckBox (Кафе)
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (chboxHotdog.IsChecked == true)           // если нажат CheckBox Хотдог
                txtboxHotdogAmount.IsReadOnly = false;   // то соотв.кол-во - доступно для записи
            else if (chboxHotdog.IsChecked == false)     // если не нажат CheckBox Хотдог
            {
                txtboxHotdogAmount.IsReadOnly = true;    // то соотв.кол-во только для чтения
                txtboxHotdogAmount.Text = 0.ToString();  // в поле кол-во устанавливаем значение 0
            }
            
            if (chboxBurger.IsChecked == true)           // следующие товары аналогично товару Хотдог
                txtboxBurgerAmount.IsReadOnly = false;
            else if(chboxBurger.IsChecked==false)
            {
                txtboxBurgerAmount.IsReadOnly = true;
                txtboxBurgerAmount.Text = 0.ToString();
            }
            
            if (chboxPotato.IsChecked == true)
                txtboxPotatoAmount.IsReadOnly = false;
            else if (chboxPotato.IsChecked==false)
            {
                txtboxPotatoAmount.IsReadOnly = true;
                txtboxPotatoAmount.Text = 0.ToString();
            }
            
            if (chboxCola.IsChecked == true)
                txtboxColaAmount.IsReadOnly = false;
            else if (chboxCola.IsChecked==false)
            {
                txtboxColaAmount.IsReadOnly = true;
                txtboxColaAmount.Text = 0.ToString();
            }
        }

        // метод Подсчет стоимости товаров кафе
        private double TotalCafe()
        {
            double total = 0;
            // если какой-то из txtbox Кол-во пустой - записываем 0
            if (txtboxHotdogAmount.Text == string.Empty)
                txtboxHotdogAmount.Text = 0.ToString();
            if (txtboxBurgerAmount.Text == string.Empty)
                txtboxBurgerAmount.Text = 0.ToString();
            if (txtboxPotatoAmount.Text == string.Empty)
                txtboxPotatoAmount.Text = 0.ToString();
            if (txtboxColaAmount.Text == string.Empty)
                txtboxColaAmount.Text = 0.ToString();

            total = (double.Parse(txtboxHotdogPrice.Text) * double.Parse(txtboxHotdogAmount.Text))
                + (double.Parse(txtboxBurgerPrice.Text) * double.Parse(txtboxBurgerAmount.Text))
                + (double.Parse(txtboxPotatoPrice.Text) * double.Parse(txtboxPotatoAmount.Text))
                + (double.Parse(txtboxColaPrice.Text) * double.Parse(txtboxColaAmount.Text));
            return total;
        }

        // событие на изменение txtbox Кол-во (Кафе)
        private void txtboxHotdogAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtboxTotalCafe.Text = TotalCafe().ToString();  // меняем итого (Кафе)
        }

        // событие на изменение txtbox Кол-во (Кафе)
        private void txtboxBurgerAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtboxTotalCafe.Text = TotalCafe().ToString();  // меняем итого (Кафе)
        }

        // событие на изменение txtbox Кол-во (Кафе)
        private void txtboxPotatoAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtboxTotalCafe.Text = TotalCafe().ToString();  // меняем итого (Кафе)
        }

        // событие на изменение txtbox Кол-во (Кафе)
        private void txtboxColaAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtboxTotalCafe.Text = TotalCafe().ToString();  // меняем итого (Кафе)
        }

        // соьытие на нажатие кнопки Посчитать
        private void btnTotal_Click(object sender, RoutedEventArgs e)
        {
            txtboxTotal.Text =                                    // меняем текст в итого (общее)
                (rdbAmountGas.IsChecked == true)                  // если rdb Кол-во (Заправка) нажата
                ? ((TotalGasStation() + TotalCafe()).ToString())  // берем эту строку, иначе следующую
                : ((TotalCafe()+double.Parse(txtboxSumGas.Text)).ToString());
        }
    }
}
