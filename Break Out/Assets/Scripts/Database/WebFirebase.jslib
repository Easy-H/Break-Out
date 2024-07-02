mergeInto(LibraryManager.library, {

    OnInit: function(path, firebaseConfigValue) {
        
        // TODO: Add SDKs for Firebase products that you want to use
        // https://firebase.google.com/docs/web/setup#available-libraries
        
        // Your web app's Firebase configuration
        // For Firebase JS SDK v7.20.0 and later, measurementId is optional

        console.log("OnInit");

        var firebaseConfig = JSON.parse(UTF8ToString(firebaseConfigValue));
        console.log(firebaseConfig);
        
        firebaseApp = firebase.initializeApp(firebaseConfig);
        auth = firebaseApp.auth();

        ref = firebase.database().ref(UTF8ToString(path));

        console.log(ref);

    },
    PostJSON: function(value, objectName, callback, fallback) {
        var parsedObjectName = UTF8ToString(objectName);
        var parsedCallback = UTF8ToString(callback);
        var parsedFallback = UTF8ToString(fallback);
        
        var newPostKey = ref.push().key;

        var postData = JSON.parse(UTF8ToString(UTF8ToString(value)));

        // Write the new post's data simultaneously in the posts list and the user's post list.
        var updates = {};
        updates['/' + newPostKey] = postData;

        ref.update(updates).then((unityInstance) => {
            window.alert("Success");
        }).catch((message) => {

        });;

    },
    AddNewScore: function(userId, score) {
        var parsed1 = UTF8ToString(userId);
        var parsed2 = UTF8ToString(score);
        
        firebase.database().ref("Leader").transaction((post) => {
            console.log(post);
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
    GetJSON: function(objectName, callback, fallback) {
        
        console.log("GetJSON");

        var parsedObjectName = UTF8ToString(objectName);
        var parsedCallback = UTF8ToString(callback);
        var parsedFallback = UTF8ToString(fallback);

        firebase.database().ref("Leader").get().then((snapshot) => {
            if (snapshot.exists()) {
                window.unityInstance.SendMessage(parsedObjectName, parsedCallback, JSON.stringify(snapshot));
                console.log(JSON.stringify(snapshot));
            } else {
                console.log("No data available");
            }
        }).catch((error) => {
            console.error(error);
        });
        
        ref.on('value', (snapshot) => {
            window.unityInstance.SendMessage(parsedObjectName, parsedCallback, JSON.stringify(snapshot));
            console.log(JSON.stringify(snapshot));
        });
        /*
        */


    },
 });