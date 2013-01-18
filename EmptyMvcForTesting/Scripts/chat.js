var chatUrl = '';
var messages = [];

var myId =''

function messageReceived(from, message, apply) {
    var scope = angular.element(document.getElementById('chatController')).scope();
    var now = new Date();
    var date = now.getHours() + ':' + now.getMinutes() + ':' + now.getSeconds();
    scope.messages.push({ message: message, from: from, date: date });
    if(apply)
        scope.$apply();
}

function sendMessage(messageTo, message) {
    $.post(chatUrl + '/SendMessageTo',
        {
            messageTo: messageTo,
            message: message
        },
        function (data) { });
}

function getUsers() {
    $.post(chatUrl + '/GetUsers',
        {},
        function (data) {
            var scope = angular.element(document.getElementById('chatController')).scope();
            for (x in data) {
                var usr = data[x];
                scope.users.push({ ClientId: usr.ClientId });
            }
            scope.$apply();
        });
    }

function simulate() {
    messageReceived('Bot','Test ');
}

function MessageCtrl($scope) {
    $scope.messages = [];
    $scope.users = [];
    $scope.sendToAll = true;
    $scope.addMessage = function () {
        var selected = $scope.selectedUser!= undefined ? $scope.selectedUser[0] : '';
        if (selected == null || selected == undefined || $scope.sendToAll)
            selected = '';
        sendMessage(selected, $scope.messagesText);
        messageReceived('Me', $scope.messagesText, false);
        $scope.messagesText = '';
    };
    
}