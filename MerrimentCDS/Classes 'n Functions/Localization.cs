/*
 *  
 *   Merriment CDS Project
 * 
 *   File: Localization.cs
 *   Purpose: Holds all localization strings
 *   
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalizationStrings
{
    class Localization
    {
        // Since we're using GDI+ to draw some strings, we're required to have a string with alot of spaces.
        public string GDIFIX = "                                                                                                                                                                                                                          ";
        
        // ============================================================================================================== 
        //                       
        // ENGLISH/US
        // Author: j0rpi
        //
        // ==============================================================================================================

        // Login Form
        public string EN_USERNAME = "Username";
        public string EN_PASSWORD = "Password";
        public string EN_LOGIN = "Login";
        public string EN_REGISTER = "Register With The Above Credentials";
        public string EN_INVALID_LOGIN = "Invalid Username/Password - Please Try Again";
        public string EN_USER_TAKEN = "This username is already taken. Please try another username.";

        // Main Form
        public string EN_LOGGED_IN_AS = "Logged in as ";
        public string EN_APP = "Application";
        public string EN_DESC = "Description";
        public string EN_VERSION = "Version";
        public string EN_INSTALL = "Install";
        public string EN_ACCCONF = "Account Settings";
        public string EN_ADMIN = "Admin Account";
        public string EN_STATUS = "Status:";
        public string EN_STATUS_IDLE = "Idle";
        public string EN_STATUS_DOWNLOADING = "Downloading";
        public string EN_DOWNLOAD_ERROR_NO_ACCESS = "Could not download selected application.";
        public string EN_DOWNLOAD_ERROR_NO_ACCESS_ACCOUNT = "Your account does not match the required account-level to use this content.";
        public string EN_DOWNLOAD_ERROR_NOT_AVAILABLE = "Could not download selected application. The application might not be available, or it has been temporarily disabled.";
        public string EN_DOWNLOAD_COMPLETE = "Download completed!";
        public string EN_APPDB_REFRESH_FAIL = "Could not refresh application database. The server might not be available or temporarily under maintenance.";

        // Account Settings Form
        public string EN_ACCCONF_AVATAR = "Avatar (64x64)";
        public string EN_ACCCONF_CHANGE_PASSWORD = "Change Password";
        public string EN_ACCCONF_SAVE_CLOSE = "Save & Close";
        public string EN_ACCCONF_AVATAR_SAVED = "Your avatar has now been updated.";
        public string EN_ACCCONF_AVATAR_FAILED = "Could not update avatar." + Environment.NewLine + "Reason: User does not exist.";
        public string EN_ACCCONF_PASSWORD_SAVED = "Your password has now been updated.";
        public string EN_ACCCONF_PASSWORD_FAILED = "Could not update password." + Environment.NewLine + "Reason: User does not exist.";

        // Generic Strings
        public string EN_ERROR = "ERROR";
        public string EN_SUCCESS = "SUCCESS";
        public string EN_LINUX = "Linux";
        public string EN_LINUX_APP = "This application is marked as a compatible Linux application." + Environment.NewLine + "This means that this application will run on Linux using WINE";
        public string EN_LINUX_COMP = "Linux Compatibility";

        
        // ============================================================================================================== 
        //                       
        // SWEDISH
        // Author: j0rpi
        //
        // ==============================================================================================================

        // Login Form
        public string SV_USERNAME = "Användarnamn";
        public string SV_PASSWORD = "Lösenord";
        public string SV_LOGIN = "Logga In";
        public string SV_REGISTER = "Registrera med speciferade värden ovan";
        public string SV_INVALID_LOGIN = "Felaktigt Användarnamn/Lösenord - Försök Igen";
        public string SV_USER_TAKEN = "Detta användarnamn är redan taget - Försök med ett annat.";

        // Main Form
        public string SV_LOGGED_IN_AS = "Inloggad som ";
        public string SV_APP = "Applikation";
        public string SV_DESC = "Beskrivning";
        public string SV_VERSION = "Version";
        public string SV_INSTALL = "Installera";
        public string SV_ACCCONF = "Konto Inställningar";
        public string SV_ADMIN = "Administratör Konto";
        public string SV_STATUS = "Status:";
        public string SV_STATUS_IDLE = "Vilar";
        public string SV_STATUS_DOWNLOADING = "Laddar Ner";
        public string SV_DOWNLOAD_ERROR_NO_ACCESS = "Kunde installera vald applikation.";
        public string SV_DOWNLOAD_ERROR_NO_ACCESS_ACCOUNT = "Ditt konto matchar inte dom krav som krävs för att använda denna applikation.";
        public string SV_DOWNLOAD_ERROR_NOT_AVAILABLE = "Kunde inte ladda ner vald applikation. Applikationen kanske inte är tillgänglig eller så är den temporärt avstängd.";
        public string SV_DOWNLOAD_COMPLETE = "Nedladdning Slutförd!";
        public string SV_APPDB_REFRESH_FAIL = "Kunde inte uppdatera applikation-databasen. Servern kanske inte är tillgänglig eller är under uppehåll.";

        // Account Settings Form
        public string SV_ACCCONF_AVATAR = "Profilbild (64x64)";
        public string SV_ACCCONF_CHANGE_PASSWORD = "Byt Lösenord";
        public string SV_ACCCONF_SAVE_CLOSE = "Spara & Stäng";
        public string SV_ACCCONF_AVATAR_SAVED = "Din profilbild har nu uppdaterats.";
        public string SV_ACCCONF_AVATAR_FAILED = "Kunde inte uppdatera profilbild." + Environment.NewLine + "Anledning: Användare existerar inte.";
        public string SV_ACCCONF_PASSWORD_SAVED = "Ditt lösenord har nu uppdaterats.";
        public string SV_ACCCONF_PASSWORD_FAILED = "Kunde inte uppdatera lösenord." + Environment.NewLine + "Anledning: Användare existerar inte.";

        // Generic Strings
        public string SV_ERROR = "FEL";
        public string SV_SUCCESS = "LYCKADES";
        public string SV_LINUX = "Linux";
        public string SV_LINUX_APP = "Denna applikation är märkt som kompitabel med Linux" + Environment.NewLine + "Detta betyder att applikationen kan köras på Linux under WINE";
        public string SV_LINUX_COMP = "Linux Kompatibilitet";
    }
}
