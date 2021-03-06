﻿using System;
using System.Diagnostics;
using Gtk;
using MonoDevelop.Core;

namespace BuildingXamarinStudioAddins
{
	public class ConfigureApiKeyDialog : Dialog
	{
		Entry apiKeyEntry;

		Label informationLabel;

		Button confirmButton;
		Button helpButton;

		public ConfigureApiKeyDialog()
		{
			const int windowWidth = 400;
			const int windowHeight = 150;
			this.WindowPosition = WindowPosition.CenterAlways;
			this.SetSizeRequest(windowWidth, windowHeight);
			this.Resizable = false;

			this.Title = "Set API Key";
			this.Events = Gdk.EventMask.AllEventsMask;
			CanFocus = true;

			VBox.SetSizeRequest(windowWidth, windowHeight);

			informationLabel = new Label("Enter your API key for Google Translation Services");
			informationLabel.Layout.Alignment = Pango.Alignment.Center;

			apiKeyEntry = new Entry();
			apiKeyEntry.CanFocus = true;
			apiKeyEntry.Events = Gdk.EventMask.AllEventsMask;
			apiKeyEntry.Text = PropertyService.Get(PropertyKeys.TranslationApiPropertyKey, "");
			apiKeyEntry.IsEditable = true;


			confirmButton = new Button();
			confirmButton.Label = "Save API Key";
			confirmButton.Clicked += (object sender, EventArgs e) =>
			{
				PropertyService.Set(PropertyKeys.TranslationApiPropertyKey, apiKeyEntry.Text);
				PropertyService.SaveProperties ();
				this.Hide();
			};

			helpButton = new Button();
			helpButton.Label = "How Do I Get An API Key?";
			helpButton.Clicked += (object sender, EventArgs e) =>
			{
				Process.Start("https://cloud.google.com/translate/docs/translating-text");
			};

			VBox.Add(informationLabel);
			VBox.Add(apiKeyEntry);
			VBox.Add(confirmButton);
			VBox.Add(helpButton);

			VBox.Events = Gdk.EventMask.AllEventsMask;
			VBox.CanFocus = true;

			ShowAll();
		}

		protected override void OnShown()
		{
			this.GrabFocus();
			apiKeyEntry.GrabFocus();
			base.OnShown();
		}
	}
}

