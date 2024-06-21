mergeInto(LibraryManager.library, {
    PostJSON: function(path, value, objectName, callback, fallback) {
        var parsedPath = Pointer_stringify(path);
        var parsedValue = Pointer_stringify(value);
        var parsedObjectName = Pointer_stringify(objectName);
        var parsedCallback = Pointer_stringify(callback);
        var parsedFallback = Pointer_stringify(fallback);
        
        var newPostKey = firebase.database().ref().child(parsedPath).push().key;

        var postData = {
            value: parsedValue
        };

        // Write the new post's data simultaneously in the posts list and the user's post list.
        var updates = {};
        updates['/' + parsedPath + '/' + newPostKey] = postData;

        firebase.database().ref().update(updates).then((unityInstance) => {
            window.alert("Success");
            //fullscreenButton.onclick = () => { unityInstance.SetFullscreen(1); };
        }).catch((message) => {
            window.alert("Fail: " + parsedPath + ", " + parsedValue+ ", " + parsedObjectName+ ", " + parsedCallback + ", " + parsedFallback);
        });;

    },
    AddNewScore: function(userId, score) {
        var parsed1 = Pointer_stringify(userId);
        var parsed2 = Pointer_stringify(score);
        
        firebase.database().ref("Leader").transaction((post) => {
            if (post) {
                var list = [];
                for (var key in post) {
                    if (post.hasOwnProperty(key)) {
                        list.push(post[key]);
                    }
                }
                list.push({"score":parsed2, "userId":parsed1});
                list.sort(function(a, b)  {
                    return a - b;
                });
                while (list.length > 20) {
                    list.shift();
                }
                var dictionary = {};
    
                for (var i = 0; i < list.length; i++) {
                    var item = list[i];
                    dictionary[i] = item;
                }
                return dictionary;
            }
            return {0:{"score":parsed2, "userId":parsed1}};
            //{"-NlrOss7BkiWpbhAljHq":{"score":"101","userId":"Pong"}}
          });

    },
    GetJSON: function(path, objectName, callback, fallback) {
        
        var parsedPath = Pointer_stringify(path);
        parsedPath = "Leader";
        var parsedObjectName = Pointer_stringify(objectName);
        var parsedCallback = Pointer_stringify(callback);
        var parsedFallback = Pointer_stringify(fallback);

        firebase.database().ref(parsedPath).get().then((snapshot) => {
            if (snapshot.exists()) {
                window.unityInstance.SendMessage(parsedObjectName, parsedCallback, JSON.stringify(snapshot));
                TestAlert(JSON.stringify(snapshot));
            } else {
                console.log("No data available");
            }
        }).catch((error) => {
            console.error(error);
        });
        
        firebase.database().ref(parsedPath).on('value', (snapshot) => {
            window.unityInstance.SendMessage(parsedObjectName, parsedCallback, JSON.stringify(snapshot));
            TestAlert(JSON.stringify(snapshot));
        });
        /*
        */


    },
 });