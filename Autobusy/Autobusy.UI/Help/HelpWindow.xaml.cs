using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Autobusy.UI.Help;

public partial class HelpWindow : Window
{
	public HelpWindow()
	{
		InitializeComponent();
	}

	private void TableOfContentsListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var headerChosen = e.AddedItems[0].ToString();
		HelpContentScrollViewer.ScrollToVerticalOffset(0);

		switch (headerChosen.ToLower())
		{
			case "wstęp":
				CreateIntroductionPanel();
				break;
			
			case "autorzy":
				CreateAuthorsPanel();
				break;
			
			case "dyspozytor":
				CreateDyspozytorPanel();
				break;
			
			case "planista":
				CreatePlanistaPanel();
				break;
			
			case "zarządca flotą":
				CreateZarzadcaFlotaPanel();
				break;
			
			case "baza danych":
				CreateDatabasePanel();
				break;
		}
	}

	private void CreateIntroductionPanel()
	{
		HelpContentPanel.Children.Clear();

		var block = new TextBlock()
		{
			FontSize = 14,
			TextWrapping = TextWrapping.Wrap,
			TextAlignment = TextAlignment.Center,
			Width = 320,
			Margin = new Thickness(20,0,40,0)
		};

		block.Inlines.AddRange(HelpStrings.StringToInlineCollection(HelpStrings.Introduction));

		HelpContentPanel.Children.Add(block);
	}
	
	private void CreateAuthorsPanel()
	{
		HelpContentPanel.Children.Clear();

		var block = new TextBlock()
		{
			FontSize = 14,
			TextWrapping = TextWrapping.Wrap,
			TextAlignment = TextAlignment.Center,
			Width = 320,
			Margin = new Thickness(20,0,40,0)
		};

		block.Inlines.AddRange(HelpStrings.StringToInlineCollection(HelpStrings.Authors));

		HelpContentPanel.Children.Add(block);
	}

	private void CreateDyspozytorPanel()
	{
		HelpContentPanel.Children.Clear();

		var block = new TextBlock()
		{
			FontSize = 14,
			TextWrapping = TextWrapping.Wrap,
			TextAlignment = TextAlignment.Center,
			Width = 320,
			Margin = new Thickness(20,0,40,0)
		};

		block.Inlines.AddRange(HelpStrings.StringToInlineCollection(HelpStrings.Dyspozytor));

		HelpContentPanel.Children.Add(block);
	}
	
	private void CreatePlanistaPanel()
	{
		HelpContentPanel.Children.Clear();

		var block = new TextBlock()
		{
			FontSize = 14,
			TextWrapping = TextWrapping.Wrap,
			TextAlignment = TextAlignment.Center,
			Width = 320,
			Margin = new Thickness(20,0,40,0)
		};

		block.Inlines.AddRange(HelpStrings.StringToInlineCollection(HelpStrings.Planista));

		HelpContentPanel.Children.Add(block);
	}
	
	private void CreateZarzadcaFlotaPanel()
	{
		HelpContentPanel.Children.Clear();

		var block = new TextBlock()
		{
			FontSize = 14,
			TextWrapping = TextWrapping.Wrap,
			TextAlignment = TextAlignment.Center,
			Width = 320,
			Margin = new Thickness(20,0,40,0)
		};

		block.Inlines.AddRange(HelpStrings.StringToInlineCollection(HelpStrings.ZarzadcaFlota));

		HelpContentPanel.Children.Add(block);
	}
	
	private void CreateDatabasePanel()
	{
		HelpContentPanel.Children.Clear();

		var block = new TextBlock()
		{
			FontSize = 14,
			TextWrapping = TextWrapping.Wrap,
			TextAlignment = TextAlignment.Center,
			Width = 320,
			Margin = new Thickness(20,0,40,0)
		};

		block.Inlines.AddRange(HelpStrings.StringToInlineCollection(HelpStrings.Database));

		HelpContentPanel.Children.Add(block);
	}
}