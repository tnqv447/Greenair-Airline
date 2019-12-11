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
            alert("Id1 " + id);
            $.ajax({
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Employee?handler=DetailEmployee',
                data: {
                    id: id
                },
                success: function (result) {
                    alert("Id2 " + result.Id);
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
            $.ajax({
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Employee?handler=EditEmployee',
                data: {
                    id: id
                },
                success: function (result) {
                    // alert("Id2 " + result.Id);
                    $("#EditEmployeeLock-id").val(result.Id);
                    $("#EditEmployeeUnlock-id").val(result.Id);
                }
            });
        });
        $("#btsubmitEditEmployeeLock").click(function () {
            var id = $('#EditEmployee-id').val();
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
            var id = $('#EditEmployee-id').val();
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