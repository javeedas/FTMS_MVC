var app = angular.module("ftms.controllers", []);
app.controller("empCtrl", function ($scope, $http, $log) {

    $scope.GetAllData = function () {
        $http({
            method: "get",
            url: "http://localhost:51362/Team/GetTeam",
        }).then(function (response) {
            $scope.employees = response.data;

        }, function () {
            alert("error occured while displayind data");
        })

    };


    $scope.DeleteEmp = function (Emp) {
        $http({
            method: "post",
            url: "http://localhost:51362/Team/Delete",
            datatype: "json",
            data: JSON.stringify(Emp)
        }).then(function (response) {
            $scope.GetAllData();
        }), function () {
            alert("error occured while deleting data")
        }
    };

    $scope.InsertData = function () {
        var action = document.getElementById("btnSave").getAttribute("value");
        if (action == "Submit") {
            $scope.emp = {};
            $scope.emp.empId = $scope.empId;
            $scope.emp.empName = $scope.empName;
            $scope.emp.category = $scope.category;
            $scope.emp.tool = $scope.tool;
            $scope.emp.designation = $scope.designation;
            $scope.emp.dob = $scope.dob;

            $http({
                method: "post",
                url: "http://localhost:51362/Team/Add",
                datatype: "json",
                data: JSON.stringify($scope.emp)
            }).then(function (response) {
                //alert("test");
                $scope.GetAllData();
                $scope.empId = "";
                $scope.empName = "";
                $scope.category = "";
                $scope.tool = "";
            })
        }
        else {
            $scope.emp = {};
            $scope.emp.empId = $scope.empId;
            $scope.emp.empName = $scope.empName;
            $scope.emp.category = $scope.category;
            $scope.emp.tool = $scope.tool;

            $http({
                method: "post",
                url: "http://localhost:51362/Team/Update",
                datatype: "json",
                data: JSON.stringify($scope.emp)
            }).then(function (response) {
                $scope.GetAllData();
                $scope.empId = "";
                $scope.empName = "";
                $scope.category = "";
                $scope.tool = "";
                document.getElementById("btnSave").setAttribute("value", "submit");
                document.getElementById("btnSave").style.backgroundColor = "cornflowerblue";
                document.getElementById("spn").innerHTML = "Add new Employee";
            })
        }
    };

    $scope.UpdateEmp = function (Emp) {
        $scope.empId = Emp.empId;
        $scope.empName = Emp.empName;
        $scope.category = Emp.category;
        $scope.tool = Emp.tool;

        document.getElementById("inputId").setAttribute("disabled", "disabled");
        document.getElementById("btnSave").setAttribute("value", "Update");
        document.getElementById("btnSave").style.backgroundColor = "yellow";
        document.getElementById("btnSave").innerHTML = "Update Employee";
    }
}

);
app.controller("loginCtrl", function ($scope, $http) {
    $scope.tools = ['CTP', 'SMM', 'QCS', 'TDG', 'FET', 'QMU'];
    $scope.selectedTool = "Select Tool";
    $scope.dropdownSelected = function (item) {
        $scope.selectedTool = item;
    };

    $scope.addUser = function () {
        $scope.user = {};
        $scope.user.name = $scope.Name;
        $scope.user.uname = $scope.userName;
        $scope.user.password = $scope.passWord;
        if ($scope.admin != '')
            $scope.user.role = $scope.Admin;
        else
            $scope.user.role = $scope.User;
        $scope.user.tool = $scope.selectedTool;

        $http({
            method: 'POST',
            url: 'http://localhost:51362/Account/AddUser',
            data: JSON.stringify($scope.user),
            datatype: 'json'
        }).then(function (response) {

            // log data to the console so we can see
            console.log(response);

            $scope.successmsg = "User Added Successfully";
            $scope.Name = null;
            $scope.userName = null;
            $scope.passWord = null;
            $scope.selectedTool = "Select Tool";
            $scope.addUserForm.$setPristine()
            // stop the form from submitting and refreshing
            event.preventDefault();

        }, function (response) {
            $scope.errormsg = "Error occured while adding user";
        });
        
               
           

    }


});

app.controller("eventCtrl", ['$scope', '$http', function ($scope, $http) {
    $scope.createEvent = function () {
        $scope.event = {};
        $scope.event.ename = $scope.eName;
        $scope.event.edate = $scope.eDate;
        $scope.event.ebudget = $scope.eBudget;

        $http({
            url: "http://localhost:51362/Event/AddEvent",
            method: "post",
            data: JSON.stringify($scope.event),
            datatype: "json"
        }).then(function (response) {
            $scope.eventCreated = true;

            $scope.eName = null;
            $scope.eDate = null;
            $scope.eBudget = null;
            $scope.eventForm.$setPristine();//reset Form
             

        }), function () {
            
            $scope.eventNotCreated = true;
        }
    
    };

    $scope.GetEvents = function () {
        $http({
            method: "get",
            url: "http://localhost:51362/Event/GetEvents",

        }).then(function (response) {
            $scope.events = response.data;
            console.log($scope.events);
            $scope.Column = $scope.events.eId;
            ////Sorting Table Data
            
            $scope.sortColumn = function (col) {
        
                        $scope.Column = col;
                        if ($scope.reverse) {
                            $scope.reverse = false;
                            $scope.reverseClass = 'arrow-up';
                        }
                        else {
                            $scope.reverse = true;
                            $scope.reverseClass = 'arrow-down';
                        }
        
                    };
            ////Applying sortclass
                        $scope.sortClass = function (col) {
                            if ($scope.Column == col) {
                                if ($scope.reverse) return 'arrow-down';
                                else return 'arrow-up';
                            }
                            else
                                return '';

                        }

        }, function () {
            alert("error occured while displayind data");
        })
    };

}]);




