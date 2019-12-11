(function ($) {
    $(function () {
        $(".DeletePlane").click(function () {
            var id = $(this).attr("id");
            if (confirm('Are you sure you want to delete this item has id: ' + id)) {
                $.ajax({
                    type: 'POST',
                    headers: {
                        "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                    },
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    url: '/Admin/Plane?handler=DeletePlane',
                    data: JSON.stringify({
                        PlaneId: id
                    }),
                    success: function (respone) {
                        alert(respone);
                        location.reload();
                    }
                });
            }
        });

        $(".EditPlane").click(function () {
            var id = $(this).attr("id");
            // alert(id);
            $.ajax({
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Plane?handler=EditPlane',
                data: {
                    id: id
                },
                success: function (result) {
                    // alert(result.PlaneId);
                    $("#EditPlane-id").val(result.PlaneId);
                    $("#EditPlane-seatnum").val(result.SeatNum);
                    $("#EditPlane-default").text(result.MakerId);

                }
            });
        });
        $("#btsubmitEditPlane").click(function () {
            var id = $('#EditPlane-id').val();
            var seatnum = $("#EditPlane-seatnum").val();
            var makerid = $("#EditPlane-makerid").val();
            event.preventDefault();
            // event.preventDefault() là để ngăn thằng form nó load lại trang ..
            $.ajax({
                type: 'POST',
                headers: {
                    "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Plane?handler=EditPlane',
                data: JSON.stringify({
                    PlaneId: id,
                    SeatNum: seatnum,
                    MakerId: makerid
                }),
                success: function (respone) {
                    $('#EditPlane').modal('hide');
                    alert(respone);
                    // $("#tablePlane").empty();
                    location.reload();
                    // $('#tablePlane').load("/Admin/Plane" + "  #tablePlane");
                },
                failure: function (result) {
                    alert("fail");
                }

            });
        });
        $("#btsubmitCreatePlane").click(function () {
            alert("Create");
            var id = $('#CreatePlane-id').val();
            var seatnum = $("#CreatePlane-seatnum").val();
            var makerid = $("#CreatePlane-makerid").val();
            event.preventDefault();
            // event.preventDefault() là để ngăn thằng form nó load lại trang ..
            $.ajax({
                type: 'POST',
                headers: {
                    "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Plane?handler=CreatePlane',
                data: JSON.stringify({
                    PlaneId: id,
                    SeatNum: seatnum,
                    MakerId: makerid
                }),
                success: function (respone) {
                    // $('#CreatePlane').modal('hide');
                    if (respone.trim() == "True") {
                        alert("Create success");
                        location.reload();
                    } else {
                        alert("This Id exists");
                        $('#CreatePlane-id').focus();
                    }
                },
                failure: function (result) {
                    alert("fail");
                }

            });
        });

    });
})(jQuery);