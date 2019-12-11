(function ($) {
    $(function () {
        $(".DeleteCustomer").click(function () {
            var id = $(this).attr("id");
            if (confirm('Are you sure you want to delete this person has id: ' + id)) {
                $.ajax({
                    type: 'POST',
                    headers: {
                        "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                    },
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    url: '/Admin/Customer?handler=DeleteCustomer',
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
        $(".DetailCustomer").click(function () {
            var id = $(this).attr("id");
            // alert("Id1 " + id);
            $.ajax({
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Customer?handler=DetailCustomer',
                data: {
                    id: id
                },
                success: function (result) {
                    // alert("Id2 " + result.Id);
                    $("#DetailCustomer-lastname").val(result.LastName);
                    $("#DetailCustomer-firstname").val(result.FirstName);
                    $("#DetailCustomer-birthday").val(result.Birthdate);
                    $("#DetailCustomer-phone").val(result.Phone);
                    $("#DetailCustomer-email").val(result.Email);
                    $("#DetailCustomer-address").val(result.Address.toString());
                    $("#DetailCustomer-status").val(result.Status);
                    $("#DetailCustomer-username").val(result.Username);
                    $("#DetailCustomer-password").val(result.Password);

                }
            });
        });
        $(".EditCustomer").click(function () {
            var id = $(this).attr("id");
            $.ajax({
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Customer?handler=EditCustomer',
                data: {
                    id: id
                },
                success: function (result) {
                    // alert("Id2 " + result.Id);
                    $("#EditCustomerLock-id").val(result.Id);
                    $("#EditCustomerUnlock-id").val(result.Id);
                }
            });
        });
        $("#btsubmitEditCustomerLock").click(function () {
            var id = $('#EditCustomerLock-id').val();
            // alert(id);
            event.preventDefault();
            // event.preventDefault() là để ngăn thằng form nó load lại trang ..
            $.ajax({
                type: 'POST',
                headers: {
                    "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Customer?handler=EditCustomerLock',
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
        $("#btsubmitEditCustomerUnlock").click(function () {
            var id = $('#EditCustomerUnlock-id').val();
            // alert(id);
            event.preventDefault();
            // event.preventDefault() là để ngăn thằng form nó load lại trang ..
            $.ajax({
                type: 'POST',
                headers: {
                    "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Customer?handler=EditCustomerUnlock',
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
        // $("#btsubmitSearchCustomer").click(function () {
        //     var search = $('#SearchCustomer').val();
        //     event.preventDefault();
        //     // event.preventDefault() là để ngăn thằng form nó load lại trang ..
        //     $.ajax({
        //         type: 'POST',
        //         headers: {
        //             "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
        //         },
        //         dataType: 'json',
        //         contentType: 'application/json; charset=utf-8',
        //         url: '/Admin/Customer?handler=EditCustomer',
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