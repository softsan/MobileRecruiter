﻿using Xamarin.Forms;
using MobileRecruiter;
using System;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections.Generic;

public class SignUpPage: ContentPage
{

	private string _baseResource;
	ActivityIndicator indicator;
	Label networkLabel;
	TapGestureRecognizer check;
	public string BaseResource
	{
		get
		{
			return _baseResource ?? (_baseResource = Assembly.GetExecutingAssembly().FullName.Split(',').FirstOrDefault());
		}
	}

	private ImageSource PlatformImageResource(string resource)
	{
		ImageSource x = ImageSource.FromResource (BaseResource + "." + resource);
		Console.WriteLine (x.ToString());
		return x;
	}

	public void UpdateUI(string Status)
	{
		indicator.IsRunning = false;
		indicator.IsVisible = false;
		networkLabel.TextColor = Color.White;
		DisplayAlert ("Congratulations", "Your Account has been Created", "Login");
		this.Navigation.PushAsync (new SignInPage ());

	}

	public SignUpPage()
	{

		this.Title = "Registration";

		check = new TapGestureRecognizer ();
		VerticalStack PageStack = new VerticalStack ();

		VerticalStack FormStack = new VerticalStack ();

		HorizontalStack NetworkStack = new HorizontalStack ();
		networkLabel = new Label ();
		networkLabel.Text = "Connecting to Server...";
		networkLabel.TextColor = Color.White;

		indicator = new ActivityIndicator();
		indicator.Color = Color.Black;
		indicator.VerticalOptions = LayoutOptions.CenterAndExpand;
		indicator.IsRunning = false;
		indicator.IsVisible = false;

		NetworkStack.Children.Add (networkLabel);
		NetworkStack.Children.Add (indicator);

		FormStack.Children.Add (NetworkStack);
		HorizontalStack EmailStack = new HorizontalStack ();
		TextLabel EmailLabel = new TextLabel ();
		EmailLabel.Text = "Email";
		TextField EmailField = new TextField ();
		EmailField.Keyboard = Keyboard.Email;
		EmailStack.Children.Add (EmailLabel);
		EmailStack.Children.Add (EmailField);

		HorizontalStack FirstNameStack = new HorizontalStack ();
		TextLabel FirstNameLabel = new TextLabel ();
		FirstNameLabel.Text = "First Name";
		TextField FirstNameField = new TextField ();
		FirstNameStack.Children.Add (FirstNameLabel);
		FirstNameStack.Children.Add (FirstNameField);

		HorizontalStack LastNameStack = new HorizontalStack ();
		TextLabel LastNameLabel = new TextLabel ();
		LastNameLabel.Text = "Last Name";
		TextField LastNameField = new TextField ();
		LastNameStack.Children.Add (LastNameLabel);
		LastNameStack.Children.Add (LastNameField);

		HorizontalStack AgencyStack = new HorizontalStack ();
		TextLabel AgencyLabel = new TextLabel ();
		AgencyLabel.Text = "Agency Name";
		TextField AgencyField = new TextField ();
		AgencyStack.Children.Add (AgencyLabel);
		AgencyStack.Children.Add (AgencyField);

		HorizontalStack PhoneStack = new HorizontalStack ();
		TextLabel PhoneLabel = new TextLabel ();
		PhoneLabel.Text = "Phone";
		TextField PhoneField = new TextField ();
		PhoneField.Keyboard = Keyboard.Telephone;
		PhoneStack.Children.Add (PhoneLabel);
		PhoneStack.Children.Add (PhoneField);

		HorizontalStack ConditionStack = new HorizontalStack ();
		TextLabel ConditionLabel = new TextLabel ();
		ConditionLabel.Text = "I Agree to the Terms & Conditions ";
		ConditionLabel.WidthRequest = 280;
		ConditionLabel.MinimumHeightRequest = 80;

		CheckedBox AgreeImage = new CheckedBox ();
		AgreeImage.Source = ImageSource.FromFile ("Uncheck.png");
		AgreeImage.GestureRecognizers.Add (check);
		AgreeImage.WidthRequest = 30;
		AgreeImage.HeightRequest = 30;


		ConditionStack.Children.Add (ConditionLabel);
		ConditionStack.Children.Add (AgreeImage);

		VerticalStack ButtonStack = new VerticalStack ();
		Button RegisterButton = new Button ();
		RegisterButton.Text = "Register";
		RegisterButton.BackgroundColor = Color.FromHex("22498a");
		RegisterButton.TextColor = Color.White;

		Button AccountHolder = new Button ();
		AccountHolder.Text = "I already have a Recruiter Account...";
		AccountHolder.BackgroundColor = Color.FromHex("3b73b9");
		AccountHolder.TextColor = Color.White;

		Button TermsButton = new Button ();
		TermsButton.Text = "Download Terms & Conditions";
		TermsButton.BackgroundColor = Color.FromHex("f7941d");
		TermsButton.TextColor = Color.White;

		Button ContactUsButton = new Button ();
		ContactUsButton.Text = "Contact Us";
		ContactUsButton.BackgroundColor = Color.FromHex("0d9c00");
		ContactUsButton.TextColor = Color.White;

		ButtonStack.Children.Add (RegisterButton);
		ButtonStack.Children.Add (AccountHolder);
		ButtonStack.Children.Add (TermsButton);
		ButtonStack.Children.Add (ContactUsButton);


		RegisterButton.Clicked += async (object sender, EventArgs e) => 
		{
			bool flag = true;

			if(string.IsNullOrEmpty(FirstNameField.Text)) {FirstNameLabel.TextColor = Color.Red; flag = false;}else  {FirstNameLabel.TextColor = Color.Black; flag = true;} 
			if(string.IsNullOrEmpty(LastNameField.Text)) { LastNameLabel.TextColor = Color.Red; flag = false;} else  { LastNameLabel.TextColor = Color.Black; flag = true;}
			if(string.IsNullOrEmpty(AgencyField.Text)) { AgencyLabel.TextColor = Color.Red; flag = false;} else  { AgencyLabel.TextColor = Color.Black; flag = true; }
			if(string.IsNullOrEmpty(PhoneField.Text)) { PhoneLabel.TextColor = Color.Red; flag = false; } else  { PhoneLabel.TextColor = Color.Black; flag = true; }

			if(string.IsNullOrEmpty(EmailField.Text))
			{
				EmailLabel.TextColor = Color.Red;
				flag = false;
			}
			else
				if(!IsValidEmail(EmailField.Text))
				{
					flag = false;
					EmailLabel.TextColor = Color.Red;
					await DisplayAlert("Invalid Email ID", "Please Enter a Valid Email ID", "Return");
				}
				else
				{
					flag =true;
					EmailLabel.TextColor = Color.Black;
					if(string.IsNullOrEmpty(FirstNameField.Text)||string.IsNullOrEmpty(LastNameField.Text)||string.IsNullOrEmpty(AgencyField.Text)||string.IsNullOrEmpty(PhoneField.Text))
					{
						flag = false;
					}
				}
			if(AgreeImage.flag == false)
			{
				await DisplayAlert("Terms & Conditions", "You need to Agree to the Terms & Conditions to Continue", "OK");
			}
			if(flag == true && AgreeImage.flag == true)
			{
				networkLabel.TextColor = Color.Blue;
				indicator.IsRunning = true;
				indicator.IsVisible = true;

				UriBuilder signUpURI = new UriBuilder ("http://134.213.136.240:1081/test.htm");
				signUpURI.Query = "&id=" + WebUtility.HtmlEncode (EmailField.Text) + WebUtility.HtmlEncode("&DeviceType=Android&DeviceOS=Android 4.4&DeviceMake=HTC&FirstName=Hal 3&LastName=Bent&Phone=11111111&AgencyName=Agent C Name&AdditionalInfo=No Info");
				HttpWebRequest signUpRequest = (HttpWebRequest)WebRequest.Create (signUpURI.Uri);

				signUpRequest.Method = WebRequestMethods.Http.Get;
				//signUpRequest.Accept = "application/json";
				Console.WriteLine("Started");

				HttpWebResponse response = await signUpRequest.GetResponseAsync() as HttpWebResponse;
				Console.WriteLine("Completed");

				if(response.StatusCode!= HttpStatusCode.OK)
				{
					Console.WriteLine("Failed");
				}
				else
				{
					Console.WriteLine("Completed");
					indicator.IsRunning = false;
					indicator.IsVisible = false;
					networkLabel.TextColor = Color.White;
					await DisplayAlert ("Congratulations", "Your Account has been Created", "Login");
					this.Navigation.PushAsync (new SignInPage ());
				}

			}

		};

		AccountHolder.Clicked += (object sender, System.EventArgs e) => 
		{
			this.Navigation.PushAsync (new SignInPage());
		};

		check.Tapped+= (object sender, EventArgs e) => 
		{
			if(AgreeImage.flag == false)
			{
				AgreeImage.flag = true;
				AgreeImage.Source = ImageSource.FromFile ("Check.png");
			}
			else
			{
				AgreeImage.flag = false;
				AgreeImage.Source = ImageSource.FromFile ("Uncheck.png");
			}
		};

		FormStack.Children.Add (EmailStack);
		FormStack.Children.Add (FirstNameStack);
		FormStack.Children.Add (LastNameStack);
		FormStack.Children.Add (AgencyStack);
		FormStack.Children.Add (PhoneStack);
		FormStack.Children.Add (ConditionStack);

		PageStack.Children.Add (FormStack);
		PageStack.Children.Add (ButtonStack);

		this.BackgroundColor = Color.White;
		Content = new ScrollView{ Content= PageStack};

	}

	public static bool IsValidEmail(string strIn)
	{
		// Return true if strIn is in valid e-mail format.
		return Regex.IsMatch(strIn,
			@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
			@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
	}


}
