// Program.cs

Console.WriteLine("Welcome to the password-based authenticator");
Console.WriteLine("");

Authenticator auth = new Authenticator();
// Authenticator auth = new AdvancedAuthenticator();

Console.WriteLine("Registering user admin");
auth.register("admin", "admindnc");

while (true) loop();

void loop() {
  Console.WriteLine("");
  string mess = "To indicate what you want to do,\n"
                   + "please type R (Register) or L (Login) or X (Exit):";
  string user_input = getUserInput(mess);
  string u_input = user_input.ToUpper();

  switch (u_input) {
    case "R":
       register();
       break;
    case "L": 
       login();
       break;
    case "X":
       exit(); 
       break;
    case "H":
       auth.hashing.hash_measurement();
       break;
    case "P":
       auth.hashing.pbkdf2_measurement();
       break;
    default:
       Console.WriteLine("Error - unrecognized input: " 
                        + user_input + " - please try again");
       break;
  }
}

// exit(), register() and login()

void exit() {
  Console.WriteLine("Exiting ..");
  Environment.Exit(0);
}

void register() {
  Console.WriteLine("Registration .. ");
  string username = getUserInput("Please type username:");
  string password = getUserInput("Please type password:");
  bool registered = auth.register(username, password);
  if (registered) Console.WriteLine("Registration succeeded");
  else Console.WriteLine("Registration failed");
}

void login() {
  Console.WriteLine("Logging in .. ");
  string username = getUserInput("Please type username:");
  string password = getUserInput("Please type password:");
  bool loggedin = auth.login(username, password);
  if (loggedin) Console.WriteLine("Login succeeded");
  else Console.WriteLine("Login failed");
}

// helper functions for exit(), register() and login()

string getUserInput(string s) {
  Console.WriteLine(s);
  return Console.ReadLine() ?? readLineError();
}


string readLineError() {
  return "Error: no string read by Console.ReadLine()";
}




   
