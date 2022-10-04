$(document).ready(function () {
    bindEvents();
    hideEmployeeDetailCard();
});

function bindEvents() {
    $(".employeeDetails").on("click", function (event) {
        var employeeId = event.currentTarget.getAttribute("data-id");

        $.ajax({
            url: 'https://localhost:6001/api/internal/employee/' + employeeId,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                var newEmployeeCard = `<div class="card">
                                          <h1>${result.name}</h1>
                                         <b>Id :</b> <p>${result.id}</p>
                                         <b>Department:</b><p>${result.department}</p>
                                         <b>Age:</b><p>${result.age}</p>
                                         <b>Address:</b><p>${result.address}</p>
                                        </div>`

                $("#EmployeeCard").html(newEmployeeCard);
                showEmployeeDetailCard();
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    $(".employeeDelete").on("click", function (event) {
        var employeeId = event.currentTarget.getAttribute("data-id");

        var result = confirm("Are You Sure You Want To Delete?");
        if (result) {
            alert("Successfully deleted the data");

            $.ajax({
                url: 'https://localhost:6001/api/internal/employee/deleteemployees/' + employeeId,
                type: 'DELETE',
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    location.reload();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
        else {
            alert("Deletion Canceled");
        }
    });

    $("#createform").submit(function (event) {

        var employeeDetailedViewModel = {};

        employeeDetailedViewModel.Name = $("#name").val();
        employeeDetailedViewModel.Department = $("#department").val();
        employeeDetailedViewModel.Age = Number($("#age").val());
        employeeDetailedViewModel.Address = $("#address").val();

        var data = JSON.stringify(employeeDetailedViewModel);

        $.ajax({
            url: 'https://localhost:6001/api/internal/employee/insertemployees',
            type: 'POST',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: data,
            success: function (result) {
                location.reload();
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
    

    $("#updateform").submit(function (event) {

        var employeeDetailedViewModel = {};

        employeeDetailedViewModel.Id = Number($("#empId").val());
        employeeDetailedViewModel.Name = $("#empName").val();
        employeeDetailedViewModel.Department = $("#empDept").val();
        employeeDetailedViewModel.Age = Number($("#empAge").val());
        employeeDetailedViewModel.Address = $("#empAddress").val();

        var data = JSON.stringify(employeeDetailedViewModel);

        $.ajax({
            url: 'https://localhost:6001/api/internal/employee/updateemployees',
            type: 'PUT',
            dataType: 'json', contentType: "application/json; charset=utf-8",
            data: data, success: function (result) {
                location.reload(true);
            },
            error: function (error) {
                console.log(error);
            }
        });
    });


    /*$("#updateForm").submit(function (event) {

        var employeeDetailedViewModel = {};

        employeeDetailedViewModel.Id = Number$("#employeeId").val();
        employeeDetailedViewModel.Name = $("#employeeName").val();
        employeeDetailedViewModel.Department = $("#employeeDepartment").val();
        employeeDetailedViewModel.Age = Number($("#employeeAge").val());
        employeeDetailedViewModel.Address = $("#employeeAddress").val();

        var data = JSON.stringify(employeeDetailedViewModel);

        $.ajax({
            url: 'https://localhost:6001/api/internal/employee/updateemployees',
            type: 'PUT',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: data,
            success: function (result) {
                location.reload();
            },
            error: function (error) {
                console.log(error);
            }
        });
    });*/

}
    function hideEmployeeDetailCard() {
        $("#EmployeeCard").hide();
    }

    function showEmployeeDetailCard() {
        $("#EmployeeCard").show();
    }


