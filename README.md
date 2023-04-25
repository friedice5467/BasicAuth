# BasicAuth

**BasicAuth** is a MAUI App for Android that covers basic authentication. It allows users to register a new user to a DB, perform form validation, login/logout, and save log-in state using tokens. It comes with an app state service that takes care of dependencies and has a popup to show users when the app is busy.

## Features

- Registering a new user to a DB
- Form Validation
- Logging in and logging out
- Saving login state using tokens
- App state service that takes care of dependencies, as well as a service that can adjust device orientation based on actions. 
- A popup that notifies users when the app is busy, boolean set in appstate

## Installation

To install and run the **BasicAuth** app, follow these steps:

1. Clone this repository to your local machine using `https://github.com/friedice5467/BasicAuth.git`
2. Open the solution in Visual Studio 2022 Preview
3. Install the required packages using NuGet Package Manager
4. Change appsettings.json to your postgres db connection string
5. Change the User model to suit your table, and update IdentityDbContext to suit your table name
6. Run the app on the Android Emulator

## Usage

With it's ease of setup and architecture, you can use this as a starting place for a MAUI app. 

## Screenshot
<p align="center">
  <img src="https://user-images.githubusercontent.com/58054670/234140266-d83cbe22-7d2a-465c-b73d-001916e201ca.png" alt="BasicAuth Screenshot">
</p>

## TODO
- Styling 
- Authorization 

## License

This project is licensed under the [MIT License](https://github.com/your-repo-link/blob/main/LICENSE).
