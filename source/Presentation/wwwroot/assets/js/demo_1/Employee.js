(function ($) {
    $(function () {
        $(".DeleteEmployee").click(function () {
            var id = $(this).attr("id");
            if (confirm('Are you sure you want to delete this person has id: ' + id)) {
                $.ajax({
                    type: 'POST',
                    headers: {
                        "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                    },
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    url: '/Admin/Employee?handler=DeleteEmployee',
                    data: JSON.stringify({
                        Id: id
                    }),
                    success: function (respone) {
                        alert(respone);
                        location.reload();
                    }
                });
            }
        });
        $(".DetailEmployee").click(function () {
            var id = $(this).attr("id");
            // alert("Id1 " + id);
            $.ajax({
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Employee?handler=DetailEmployee',
                data: {
                    id: id
                },
                success: function (result) {
                    // alert("Id2 " + result.Id);
                    $("#DetailEmployee-lastname").val(result.LastName);
                    $("#DetailEmployee-firstname").val(result.FirstName);
                    $("#DetailEmployee-birthday").val(result.Birthdate);
                    $("#DetailEmployee-phone").val(result.Phone);
                    $("#DetailEmployee-job").val(result.JobId);
                    $("#DetailEmployee-salary").val(result.Salary);
                    $("#DetailEmployee-address").val(result.Address.toString());
                    $("#DetailEmployee-status").val(result.Status);
                    $("#DetailEmployee-username").val(result.Username);
                    $("#DetailEmployee-password").val(result.Password);

                }
            });
        });
        $(".EditEmployee").click(function () {
            var id = $(this).attr("id");
            // alert(id);
            $.ajax({
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Employee?handler=EditEmployee',
                data: {
                    id: id
                },
                success: function (result) {
                    // alert("Id2 " + result.id);
                    $("#EditEmployeeLock-id").val(result.id);
                    $("#EditEmployeeUnlock-id").val(result.id);
                }
            });
        });
        $("#btsubmitEditEmployeeLock").click(function () {
            var id = $('#EditEmployeeLock-id').val();
            event.preventDefault();
            // event.preventDefault() là để ngăn thằng form nó load lại trang ..
            $.ajax({
                type: 'POST',
                headers: {
                    "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Employee?handler=EditEmployeeLock',
                data: JSON.stringify({
                    Id: id
                }),
                success: function (respone) {
                    alert("Disabled success");
                    location.reload();
                },
                failure: function (result) {
                    alert("fail");
                }

            });
        });
        $("#btsubmitEditEmployeeUnlock").click(function () {
            var id = $('#EditEmployeeUnlock-id').val();
            event.preventDefault();
            // event.preventDefault() là để ngăn thằng form nó load lại trang ..
            $.ajax({
                type: 'POST',
                headers: {
                    "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Employee?handler=EditEmployeeUnlock',
                data: JSON.stringify({
                    Id: id
                }),
                success: function (respone) {
                    alert("Active success");
                    location.reload();
                },
                failure: function (result) {
                    alert("fail");
                }

            });
        });
        $("#btsubmitCreateEmployee").click(function () {
            // alert("Create");
            var check = true;
            var test = true;
            // var id = $('#CreateEmployee-id').val();
            var firstname = $("#CreateEmployee-firstname").val();
            var lastname = $("#CreateEmployee-lastname").val();
            var username = $("#CreateEmployee-username").val();
            var password = $("#CreateEmployee-password").val();
            var password2 = $("#CreateEmployee-password2").val();
            var birthdate = $("#CreateEmployee-birthdate").val();
            var phone = $("#CreateEmployee-phone").val();
            var job = $("#CreateEmployee-job").val().slice(0, 3);
            var status = $("#CreateEmployee-status").val();
            var salary = $("#CreateEmployee-salary").val();
            var address = $("#CreateEmployee-address").val();
            alert(username + " " + password + " " + password2 + " " + birthdate);
            if (username.length == 0 || username.length > 20) {
                $("#z-CreateEmployee-username").removeClass("hidden-class");
                check = false;
            } else {
                $("#z-CreateEmployee-username").addClass("hidden-class");
            }
            if (password.length == 0 || password.length > 20) {
                $("#z-CreateEmployee-password").removeClass("hidden-class");
                check = false;
            } else {
                $("#z-CreateEmployee-password").addClass("hidden-class");
                if (password2 == null || (password2 != password)) {
                    $("#z-CreateEmployee-password2").removeClass("hidden-class");
                    check = false;
                } else {
                    $("#z-CreateEmployee-password2").addClass("hidden-class");
                }
            }
            var patt = /[A-Za-z\s]+/;
            test = patt.test(firstname);
            if (test == false) {
                $("#z-CreateEmployee-firstname").removeClass("hidden-class");
                check = false;
            } else {
                $("#z-CreateEmployee-firstname").addClass("hidden-class");
            }
            test = patt.test(lastname);
            if (test == false) {
                $("#z-CreateEmployee-lastname").removeClass("hidden-class");
                check = false;
            } else {
                $("#z-CreateEmployee-lastname").addClass("hidden-class");
            }
            var patt = /0[0-9]{9,10}/;
            test = patt.test(phone);
            if (test == false) {
                $("#z-CreateEmployee-phone").removeClass("hidden-class");
                check = false;
            } else {
                $("#z-CreateEmployee-phone").addClass("hidden-class");
            }
            if (birthdate.length == 0) {
                $("#z-CreateEmployee-birthdate").removeClass("hidden-class");
                check = false;
            } else {
                $("#z-CreateEmployee-birthdate").addClass("hidden-class");
            }
            if (check == false) {
                alert("Fail");
                return;
            } else {
                alert("Well");
            }

            event.preventDefault();
            // event.preventDefault() là để ngăn thằng form nó load lại trang ..
            $.ajax({
                type: 'POST',
                headers: {
                    "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Employee?handler=CreateEmployee',
                data: JSON.stringify({
                    FirstName: firstname,
                    LastName: lastname,
                    Username: username,
                    Password: password,
                    Birthdate: birthdate,
                    Phone: phone,
                    JobId: job,
                    Salary: salary,
                    Status: status,
                    Address: address
                }),
                success: function (respone) {
                    // $('#CreateEmployee').modal('hide');
                    if (respone.trim() == "True") {
                        alert("Create success");
                        location.reload();
                    } else {
                        alert("This Id exists");
                        $('#CreateEmployee-username').focus();
                    }
                },
                failure: function (result) {
                    alert("fail");
                }

            });
        });
        // $("#btsubmitSearchEmployee").click(function () {
        //     var search = $('#SearchEmployee').val();
        //     event.preventDefault();
        //     // event.preventDefault() là để ngăn thằng form nó load lại trang ..
        //     $.ajax({
        //         type: 'POST',
        //         headers: {
        //             "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
        //         },
        //         dataType: 'json',
        //         contentType: 'application/json; charset=utf-8',
        //         url: '/Admin/Employee?handler=EditEmployee',
        //         data: {
        //             searchString: search
        //         },
        //         success: function (respone) {
        //             location.reload();
        //         },
        //         failure: function (result) {
        //             alert("fail");
        //         }

        //     });
        // });

    });
})(jQuery);