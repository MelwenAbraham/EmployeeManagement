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

                /*location.reload();*/
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    $(".employeeEdit").on("click", function (event) {
        console.log("clicked");
        var employeeId = event.currentTarget.getAttribute("data-id");

        $.ajax({
            url: 'https://localhost:6001/api/internal/employee/' + employeeId,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                $("#employeeId").val(result.id)
                $("#employeeName").val(result.name)
                $("#employeeDept").val(result.department)
                $("#employeeAge").val(result.age)
                $("#employeeAddress").val(result.address)
            },
            error: function (error) {
                console.log(error);
            }
        });
        $("#updateform").submit(function (event) {
            console.log("clicked");
            var idUpdate = $("#employeeId").val();
            var nameUpdate = $("#employeeName").val();
            var departmentUpdate = $("#employeeDept").val();
            var ageUpdate = $("#employeeAge").val();
            var addressUpdate = $("#employeeAddress").val();

            let employees = {
                id: parseInt(idUpdate),
                name: nameUpdate,
                department: departmentUpdate,
                age: parseInt(ageUpdate),
                address: addressUpdate
            };
            $.ajax({
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                url: 'https://localhost:6001/api/internal/employee/updateemployees',
                type: 'PUT',
                data: JSON.stringify(employees),
                dataType: 'json',
                success: function (result) {

                    location.reload();
                },
                error: function (error) {
                    console.log(error);
                }
            });
        });
    });
}
function hideEmployeeDetailCard() {
    $("#EmployeeCard").hide();
}

function showEmployeeDetailCard() {
    $("#EmployeeCard").show();
}


