var appFilters = angular.module('ftms.Filters', []);
appFilters.filter('DateFilter', function () {
    var regex = /\/Date\(([0-9]*)\)\//;
    return function (jsondate) {
        var date = jsondate.match(regex);
        if (date)
            return new Date(parseInt(date[1]));
        else
            return null;
    };
});
appFilters.filter('SearchFilter', function () {
    return function (events, esearchText) {
        //var filtered = [];
        //esearchText = esearchText.toLowerCase();
        //angular.forEach(events, function (Event) {
        //    if (Event.eName === esearchText) { // matches whole word, e.g. 'England'
        //        filtered.push(Event);
        //    }
        //    var letters = Event.eName.split('');
        //    angular.forEach(letters, function (letter) {
        //        if (letter === esearchText) { // matches single letter, e.g. 'E'
        //            console.log('pushing');
        //            filtered.push(Event);
        //        }
        //    });
        //    // code to match letter fragments, e.g. 'lan'
        //    angular.forEach(events, function (Event) {
        //        if (Event.eName.toLowerCase().indexOf(esearchText) >= 0) filtered.push(Event);
        //    });
        //});
        var filtered = [];
        for (var i = 0; i < events.length; i++) {
            var item = events[i];
            if (item.substr(0, esearchText.length).toLowerCase() == esearchText.toLowerCase()) {
                filtered.push(item);
            }
        }
        return filtered;
        //return filtered;
    };
});
//appFiletr.filter("SortColumns", function () {
//    return function (col,reverse) {
//        $scope.Column = $scope.events.eId;
//        $scope.reverse = false;
//        $scope.sortColumn = function (col) {
//            $scope.Column = col;
//            if ($scope.reverse) {
//                $scope.reverse = false;
//                $scope.reverseClass = 'arrow-up';
//            }
//            else {
//                $scope.reverse = true;
//                $scope.reverseClass = 'arrow-down';
//            }

//        };

//        $scope.sortClass = function (col) {
//            if ($scope.Column == col) {
//                if ($scope.reverse) return 'arrow-down';
//                else return 'arrow-up';
//            }
//            else
//                return '';

//        }
        
//    };

  

//});
