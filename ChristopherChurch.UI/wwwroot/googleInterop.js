window.googleInterop = {
    getGoogleUserToken: async function () {
        try {
            console.log("Before gapi.load");
            await new Promise(resolve => gapi.load('auth2', resolve));
            console.log("After gapi.load");

            const authInstance = await gapi.auth2.init({
                client_id: GoogleApiSettings.ClientId,
                cookie_policy: 'single_host_origin',
            });
            console.log("After gapi.auth2.init");

            const user = authInstance.currentUser.get();
            const token = user.getAuthResponse().id_token;
            return token;
        } catch (error) {
            console.error("Error in getGoogleUserToken:", error);
            throw error;
        }
    }
};


