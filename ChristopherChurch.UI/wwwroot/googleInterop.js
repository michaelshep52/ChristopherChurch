window.googleInterop = {
    authorizeGoogleCalendar: async function () {
        try {
            console.log("Before gapi.load");
            await new Promise(resolve => gapi.load('auth2', resolve));
            console.log("After gapi.load");

            const authInstance = await gapi.auth2.init({
                client_id: GoogleApiSettings.ClientId,
                cookie_policy: 'single_host_origin',
                scope: 'https://www.googleapis.com/auth/calendar.events', 
            });
            console.log("After gapi.auth2.init");

            const user = await authInstance.signIn();
            console.log("After signIn");

            if (user && user.isSignedIn()) {
                console.log("User is signed in");
                return { isAuthorized: true };
            } else {
                console.log("User is not signed in");
                return { isAuthorized: false };
            }
        } catch (error) {
            console.error("Error in authorizeGoogleCalendar:", error);
            return { isAuthorized: false, error: error.message };
        }
    }
};

