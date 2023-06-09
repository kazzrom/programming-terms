﻿using ProgTerms.AppData;
using ProgTerms.Controllers;
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

namespace ProgTerms.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageEditTerm.xaml
    /// </summary>
    public partial class PageEditTerm : Page
    {
        private bool IsFavorite { get; set; }
        public PageEditTerm(bool isFavorite)
        {
            InitializeComponent();
            IsFavorite = isFavorite;

            WTBTitle.Text = CurrentTerm.Term.Title;
            WTBDefinition.Text = CurrentTerm.Term.Definition; 
            WTBAddInfo.Text = CurrentTerm.Term.AddInformation;
        }

        private void BtnSaveTerm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(WTBTitle.Text) || string.IsNullOrEmpty(WTBDefinition.Text))
                {
                    WTBTitle.BorderBrush = string.IsNullOrEmpty(WTBTitle.Text) ? Brushes.Red : Brushes.Gray;
                    WTBDefinition.BorderBrush = string.IsNullOrEmpty(WTBDefinition.Text) ? Brushes.Red : Brushes.Gray;
                    throw new Exception("Введите обязательные поля!");
                }

                var MBisSave = MessageBox.Show("Сохранить изменения?", "Изменение термина", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if(MBisSave == MessageBoxResult.Yes)
                {
                    CurrentTerm.Term.Title = WTBTitle.Text;
                    CurrentTerm.Term.Definition = WTBDefinition.Text;
                    CurrentTerm.Term.AddInformation = WTBAddInfo.Text;

                    ConnectDB.ProgTermsContext.SaveChanges();

                    if(IsFavorite)
                    {
                        MainObjects.FrameMain.Navigate(new PageFavoritesTerms());
                        MainObjects.ChangeHeaderObjects("pack://application:,,,/Icons/Bookmark.ico", "Избранное");
                    }
                    else
                    {
                        MainObjects.FrameMain.Navigate(new PageAllTerms());
                        MainObjects.ChangeHeaderObjects("pack://application:,,,/Icons/Open Book.ico", "Все термины");
                    }
                }
                else if (MBisSave == MessageBoxResult.No)
                {
                    if (IsFavorite)
                    {
                        MainObjects.FrameMain.Navigate(new PageFavoritesTerms());
                        MainObjects.ChangeHeaderObjects("pack://application:,,,/Icons/Bookmark.ico", "Избранное");
                    }
                    else
                    {
                        MainObjects.FrameMain.Navigate(new PageAllTerms());
                        MainObjects.ChangeHeaderObjects("pack://application:,,,/Icons/Open Book.ico", "Все термины");
                    }
                }
                MainObjects.BtnBack.Visibility = Visibility.Visible;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
