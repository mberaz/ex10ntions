 var Notificator = {};
 Notificator = function () {
    var pub = {};
    var self = {};
    pub.showNotification = function (body, title, tsgName, icon, timeout, onClickCallbac) {
        // Let's check if the browser supports notifications
        if (!('Notification' in window)) {
            alert('This browser does not support desktop notification');
        }

            // Let's check whether notification permissions have already been granted
        else if (Notification.permission === pub.permissionTypes.granted) {
            // If it's okay let's create a notification
            self.spawnNotification(body, icon, title, tsgName, timeout, onClickCallbac);
        }
            // Otherwise, we need to ask the user for permission
        else if (Notification.permission !== pub.permissionTypes.denied) {
            Notification.requestPermission(function (permission) {
                // If the user accepts, let's create a notification
                if (permission === pub.permissionTypes.granted) {
                    self.spawnNotification(body, icon, title, tsgName, timeout, onClickCallbac);
                }
            });
        }

        //At last, if the user has denied notifications, and you want to be respectful there is no need to bother them any more.
    }
    pub.permissionTypes = {
        granted: 'granted',
        denied: 'denied'
    };

    self.spawnNotification = function (body, icon, title, tag, timeout, onClickCallback) {
        const notification = new Notification(title, {
            body: body,
            icon: icon || '',
            tag: tag || ''
        });

        if (onClickCallback) {
            notification.onclick = function (event) {
                event.preventDefault(); // prevent the browser from focusing the Notification's tab
                onClickCallback(event.target.tag, event.target.title, event.target.body, event);
            }
        }

        setTimeout(notification.close.bind(notification), timeout);
    }

    return pub;
}();