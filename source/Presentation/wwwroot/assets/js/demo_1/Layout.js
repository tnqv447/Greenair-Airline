(function ($) {
    $(function () {
        loadUser();

        function loadUser() {
            $.ajax({
                type: 'GET',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                url: '/Admin/Account?handler=User',
                success: function (response) {
                    if ($.trim(response.check) == "not") {

                        $("#personName").text(response.name);
                    }
                },
                error: function (response) {
                    alert("false");
                }
            })
        }
        $("#signout").on("click", function () {
            $.ajax({
                type: "POST",
                url: "/Admin/Account?handler=LogOut",
                headers: {
                    "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                success: function (response) {
                    loadUser();
                    location.replace("/admin");
                    // location.reload();
                }
            });
        });
        
            $(document).on("click",".EditRoute",function(){
                var id = $(this).attr("id");
                $.ajax({
                    type:"GET",
                    url:"/Admin/route?handler=EditRoute",
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    data:{
                        id: id
                    },
                    success: function(reponse)
                    {
                        $("#EditRoute-origin").val(reponse.Origin);
                        $("#EditRoute-destination").val(reponse.Destination);
                        $("#EditRoute-hour").val(reponse.FlightTime["Hour"]);
                        $("#EditRoute-minute").val(reponse.FlightTime["Minute"]);
                        $(".hidden-id").val(reponse.RouteId);
                    }
                })
            });
            $(document).on("click","#btsubmitEditRoute",function(){
                var id = $(".hidden-id").val();
                var origin = $("#EditRoute-origin").val();
                var destination = $("#EditRoute-destination").val();
                var Hour = parseInt($("#EditRoute-hour").val());
                var Minute = parseInt($("#EditRoute-minute").val());
                event.preventDefault();
                if(origin == destination)
                {
                    $("#err-airport").removeClass("hidden-class");
                }
                else{
                    $("#err-airport").addClass("hidden-class");
                    if(Hour == 0 && Minute == 0)
                    {
                        $("#err-time").removeClass("hidden-class");
                    }
                    else{
                        $("#err-time").addClass("hidden-class");
                        $.ajax({
                            type:"POST",
                            headers: {
                                "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                            },
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            url:"/Admin/Route?Handler=EditRoute",
                            data:JSON.stringify({
                                RouteId: id,
                                Origin: origin,
                                Destination: destination,
                                Hour: Hour,
                                Minute: Minute
                            }),
                            success:function(reponse){
                                alert(reponse);
                                location.reload();
                            }
                        });
                    }
                
                }
                
                
            });
            $(document).on("click","#btsubmitCreateRoute",function(){
                var origin = $("#CreateRoute-origin").val();
                var destination = $("#CreateRoute-destination").val();
                var Hour = parseInt($("#CreateRoute-hour").val());
                var Minute = parseInt($("#CreateRoute-minute").val());
                event.preventDefault();
                if(origin == destination)
                {
                    $("#err-airport-create").removeClass("hidden-class");
                }
                else{
                    $("#err-airport-create").addClass("hidden-class");
                    if(Hour == 0 && Minute == 0)
                    {
                        $("#err-time-create").removeClass("hidden-class");
                    }
                    else{
                        $("#err-time-create").addClass("hidden-class");
                        $.ajax({
                            type:"POST",
                            headers: {
                                "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                            },
                            dataType: 'json',
                            contentType: 'application/json; charset=utf-8',
                            url:"/Admin/Route?Handler=CreateRoute",
                            data:JSON.stringify({
                                Origin: origin,
                                Destination: destination,
                                Hour: Hour,
                                Minute: Minute
                            }),
                            success:function(reponse){
                                alert(reponse);
                                location.reload();
                            }
                        });
                    }
                }
                
            });
            $(document).on("click",".DeleteRoute",function(){
                var id =$(this).attr("id");
                if (confirm('Are you sure you want to delete this item has id: ' + id)) {
                    $.ajax({
                        type:"POST",
                        headers: {
                            "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                        },
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        url:"/Admin/Route?Handler=DeleteRoute",
                        data:JSON.stringify({
                            RouteId: id,
                        }),
                        success:function(reponse){
                            alert(reponse);
                            location.reload();
                        }
                    });
                }
            });
    });
})(jQuery);