
     
        let params = 'scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,width=600,height=300,left=100,top=100';
        let popupName = 'doredo';
        let popupRef = null;
        let popupWatcher = null;
        const POPUP_KEY = 'popup_window_open';
        const POPUP_URL_KEY = 'popup_window_url';
        const bc = new BroadcastChannel('popup_channel');

        bc.onmessage = function(event) {
            if (event.data === 'focus') {
                if (popupRef && !popupRef.closed) {
                    popupRef.focus();
                }
            }
            if (event.data === 'reload') {
                if (popupRef && !popupRef.closed) {
                    popupRef.location.reload();
                    popupRef.focus();
                }
            }
        };

        function openOnce(url) {
            const absoluteUrl = new URL(url, window.location.origin).href;
            const popupState = localStorage.getItem(POPUP_KEY);
            const existingUrl = localStorage.getItem(POPUP_URL_KEY);
            const isSameUrl = (existingUrl === absoluteUrl);

            if (!popupState || popupState === 'closed') {
                popupRef = window.open(absoluteUrl, popupName, params);
                if (popupRef) {
                    localStorage.setItem(POPUP_KEY, 'open');
                    localStorage.setItem(POPUP_URL_KEY, absoluteUrl);
                    watchPopup();
                }
            } else {
                if (!isSameUrl) {
                    localStorage.setItem(POPUP_URL_KEY, absoluteUrl);
                    bc.postMessage('reload');
                } else {
                    bc.postMessage('focus');
                }
            }
        }

        function watchPopup() {
            if (popupWatcher) clearInterval(popupWatcher);

            popupWatcher = setInterval(() => {
                if (popupRef && popupRef.closed) {
                    clearInterval(popupWatcher);
                    popupWatcher = null;
                    localStorage.setItem(POPUP_KEY, 'closed');
                    hello();
                }
            }, 500);
        }

        window.addEventListener('storage', (event) => {
            if (event.key === POPUP_KEY && event.newValue === 'closed') {
                console.log('Popup başka sekmede kapandı.');
            }
        });

        var url = '../map/MapView.aspx';
        openOnce(url);
      
    