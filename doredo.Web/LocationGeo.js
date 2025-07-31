
let params = 'scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,width=600,height=300,left=100,top=100';
let popupName = 'doredo';
let popupRef = null;
const POPUP_KEY = 'popup_window_open';
const POPUP_URL = 'popup_window_url';
const bc = new BroadcastChannel('popup_channel');

// Sayfa reload olursa popup'a tekrar ulaş
window.addEventListener('load', () => {
    if (sessionStorage.getItem(POPUP_KEY) === 'true') {
        try {
            popupRef = window.open('', popupName);
            if (popupRef && !popupRef.closed) {
                const savedUrl = sessionStorage.getItem(POPUP_URL);
                if (savedUrl && popupRef.location.href !== savedUrl) {
                    popupRef.location.href = savedUrl;
                } else {
                    popupRef.location.reload();
                }
            }
        } catch (e) {
            console.warn('Popup geri yüklenemedi:', e);
        }
    }
});

bc.onmessage = function(event) {
    if (event.data === 'focus') {
        if (popupRef && !popupRef.closed) popupRef.focus();
    }
};

function openOnce(url) {
    const absoluteUrl = new URL(url, window.location.origin).href;

    try {
        // Popup zaten açık mı kontrolü
        if (sessionStorage.getItem(POPUP_KEY) === 'true') {
            popupRef = window.open('', popupName);
            if (popupRef && !popupRef.closed) {
                if (popupRef.location.href === absoluteUrl) {
                    popupRef.location.reload();
                } else {
                    popupRef.location.href = absoluteUrl;
                }
                popupRef.focus();
                bc.postMessage('focus');
                return;
            }
        }
    } catch (e) {
        console.error('Popup kontrol hatası:', e);
    }

    // Yeni popup aç
    popupRef = window.open(absoluteUrl, popupName, params);

    if (popupRef) {
        sessionStorage.setItem(POPUP_KEY, 'true');
        sessionStorage.setItem(POPUP_URL, absoluteUrl);

        const popupWatcher = setInterval(() => {
            if (popupRef.closed) {
                clearInterval(popupWatcher);
                sessionStorage.setItem(POPUP_KEY, 'false');
                sessionStorage.removeItem(POPUP_URL);
                bc.postMessage('closed');

                // ASPX'e kapanma bildirimi
                fetch('/PopupClosed.aspx', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ message: 'popup_closed' })
                });
            }
        }, 500);
    }

    popupRef.focus();
    bc.postMessage('focus');
}

// Başlat
var url = '../../map/MapView.aspx';
openOnce(url);