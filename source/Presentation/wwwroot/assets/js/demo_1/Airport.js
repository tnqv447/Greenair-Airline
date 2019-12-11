(function ($) {
    $(function () {
        // $('input').blur(function () {
        //     $(this).css("border-color", "#ced4da");
        // });
        $(".DeleteAirport").click(function () {
            var id = $(this).attr("id");
            if (confirm('Are you sure you want to delete this item has id: ' + id)) {
                $.ajax({
                    type: 'POST',
                    headers: {
                        "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                    },
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    url: '/Admin/Airport?handler=DeleteAirport',
                    data: JSON.stringify({
                        AirportId: id
                    }),
                    success: function (respone) {
                        alert(respone);
                        location.reload();
                    }
                });
            }
        });

        $(".EditAirport").click(function () {
            var id = $(this).attr("id");
            $.ajax({
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Airport?handler=EditAirport',
                data: {
                    id: id
                },
                success: function (result) {
                    $("#EditAirport-id").val(result.AirportId);
                    $("#EditAirport-name").val(result.AirportName);
                    $("#EditAirport-address").val(result.Address);
                }
            });
        });
        $("#btsubmitEditAirport").click(function () {
            var id = $('#EditAirport-id').val();
            var name = $("#EditAirport-name").val();
            var address = $("#EditAirport-address").val();
            event.preventDefault();
            // event.preventDefault() là để ngăn thằng form nó load lại trang ..
            $.ajax({
                type: 'POST',
                headers: {
                    "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Airport?handler=EditAirport',
                data: JSON.stringify({
                    AirportId: id,
                    AirportName: name,
                    Address: address
                }),
                success: function (respone) {
                    $('#EditAirport').modal('hide');
                    alert(respone);
                    // $("#tableAirport").empty();
                    location.reload();
                    // $('#tableAirport').load("/Admin/Airport" + "  #tableAirport");
                },
                failure: function (result) {
                    alert("fail");
                }

            });
        });
        $("#btsubmitCreateAirport").click(function () {
            var id = $('#CreateAirport-id').val();
            var name = $("#CreateAirport-name").val();
            var address = $("#CreateAirport-address").val();
            event.preventDefault();
            // event.preventDefault() là để ngăn thằng form nó load lại trang ..
            $.ajax({
                type: 'POST',
                headers: {
                    "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Airport?handler=CreateAirport',
                data: JSON.stringify({
                    AirportId: id,
                    AirportName: name,
                    Address: address
                }),
                success: function (respone) {
                    // $('#CreateAirport').modal('hide');
                    if (respone.trim() == "True") {
                        alert("Create success");
                        location.reload();
                    } else {
                        alert("This Id exists");
                        $('#CreateAirport-id').focus();
                    }
                },
                failure: function (result) {
                    alert("fail");
                }

            });
        });
    });
})(jQuery);