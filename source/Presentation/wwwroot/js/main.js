 AOS.init({
 	duration: 800,
 	easing: 'slide'
 });

 (function ($) {

 	"use strict";

 	var isMobile = {
 		Android: function () {
 			return navigator.userAgent.match(/Android/i);
 		},
 		BlackBerry: function () {
 			return navigator.userAgent.match(/BlackBerry/i);
 		},
 		iOS: function () {
 			return navigator.userAgent.match(/iPhone|iPad|iPod/i);
 		},
 		Opera: function () {
 			return navigator.userAgent.match(/Opera Mini/i);
 		},
 		Windows: function () {
 			return navigator.userAgent.match(/IEMobile/i);
 		},
 		any: function () {
 			return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
 		}
 	};


 	$(window).stellar({
 		responsive: true,
 		parallaxBackgrounds: true,
 		parallaxElements: true,
 		horizontalScrolling: false,
 		hideDistantElements: false,
 		scrollProperty: 'scroll'
 	});


 	var fullHeight = function () {

 		$('.js-fullheight').css('height', $(window).height());
 		$(window).resize(function () {
 			$('.js-fullheight').css('height', $(window).height());
 		});

 	};
 	fullHeight();

 	// loader
 	var loader = function () {
 		setTimeout(function () {
 			if ($('#ftco-loader').length > 0) {
 				$('#ftco-loader').removeClass('show');
 			}
 		}, 1);
 	};
 	loader();

 	// Scrollax
 	$.Scrollax();

 	var carousel = function () {
 		$('.carousel-testimony').owlCarousel({
 			center: true,
 			loop: true,
 			items: 1,
 			margin: 30,
 			stagePadding: 0,
 			nav: true,
 			navText: ['<span class="ion-ios-arrow-back">', '<span class="ion-ios-arrow-forward">'],
 			responsive: {
 				0: {
 					items: 1
 				},
 				600: {
 					items: 3
 				},
 				1000: {
 					items: 3
 				}
 			}
 		});

 		$('.single-slider').owlCarousel({
 			animateOut: 'fadeOut',
 			animateIn: 'fadeIn',
 			autoplay: true,
 			loop: true,
 			items: 1,
 			margin: 0,
 			stagePadding: 0,
 			nav: true,
 			dots: true,
 			navText: ['<span class="ion-ios-arrow-back">', '<span class="ion-ios-arrow-forward">'],
 			responsive: {
 				0: {
 					items: 1
 				},
 				600: {
 					items: 1
 				},
 				1000: {
 					items: 1
 				}
 			}
 		});

 	};
 	carousel();

 	$('nav .dropdown').hover(function () {
 		var $this = $(this);
 		// 	 timer;
 		// clearTimeout(timer);
 		$this.addClass('show');
 		$this.find('> a').attr('aria-expanded', true);
 		// $this.find('.dropdown-menu').addClass('animated-fast fadeInUp show');
 		$this.find('.dropdown-menu').addClass('show');
 	}, function () {
 		var $this = $(this);
 		// timer;
 		// timer = setTimeout(function(){
 		$this.removeClass('show');
 		$this.find('> a').attr('aria-expanded', false);
 		// $this.find('.dropdown-menu').removeClass('animated-fast fadeInUp show');
 		$this.find('.dropdown-menu').removeClass('show');
 		// }, 100);~
 	});


 	$('#dropdown04').on('show.bs.dropdown', function () {
 		console.log('show');
 	});

 	// scroll
 	var scrollWindow = function () {
 		$(window).scroll(function () {
 			var $w = $(this),
 				st = $w.scrollTop(),
 				navbar = $('.ftco_navbar'),
 				sd = $('.js-scroll-wrap');

 			if (st > 150) {
 				if (!navbar.hasClass('scrolled')) {
 					navbar.addClass('scrolled');
 				}
 			}
 			if (st < 150) {
 				if (navbar.hasClass('scrolled')) {
 					navbar.removeClass('scrolled sleep');
 				}
 			}
 			if (st > 350) {
 				if (!navbar.hasClass('awake')) {
 					navbar.addClass('awake');
 				}

 				if (sd.length > 0) {
 					sd.addClass('sleep');
 				}
 			}
 			if (st < 350) {
 				if (navbar.hasClass('awake')) {
 					navbar.removeClass('awake');
 					navbar.addClass('sleep');
 				}
 				if (sd.length > 0) {
 					sd.removeClass('sleep');
 				}
 			}
 		});
 	};
 	scrollWindow();

 	var isMobile = {
 		Android: function () {
 			return navigator.userAgent.match(/Android/i);
 		},
 		BlackBerry: function () {
 			return navigator.userAgent.match(/BlackBerry/i);
 		},
 		iOS: function () {
 			return navigator.userAgent.match(/iPhone|iPad|iPod/i);
 		},
 		Opera: function () {
 			return navigator.userAgent.match(/Opera Mini/i);
 		},
 		Windows: function () {
 			return navigator.userAgent.match(/IEMobile/i);
 		},
 		any: function () {
 			return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
 		}
 	};


 	var counter = function () {

 		$('#section-counter').waypoint(function (direction) {

 			if (direction === 'down' && !$(this.element).hasClass('ftco-animated')) {

 				var comma_separator_number_step = $.animateNumber.numberStepFactories.separator(',')
 				$('.number').each(function () {
 					var $this = $(this),
 						num = $this.data('number');
 					console.log(num);
 					$this.animateNumber({
 						number: num,
 						numberStep: comma_separator_number_step
 					}, 7000);
 				});

 			}

 		}, {
 			offset: '95%'
 		});

 	}
 	counter();

var contentWayPoint = function () {
var i = 0;
$('.ftco-animate').waypoint(function (direction) {

	if (direction === 'down' && !$(this.element).hasClass('ftco-animated')) {

		i++;

		$(this.element).addClass('item-animate');
		setTimeout(function () {

			$('body .ftco-animate.item-animate').each(function (k) {
				var el = $(this);
				setTimeout(function () {
					var effect = el.data('animate-effect');
					if (effect === 'fadeIn') {
						el.addClass('fadeIn ftco-animated');
					} else if (effect === 'fadeInLeft') {
						el.addClass('fadeInLeft ftco-animated');
					} else if (effect === 'fadeInRight') {
						el.addClass('fadeInRight ftco-animated');
					} else {
						el.addClass('fadeInUp ftco-animated');
					}
					el.removeClass('item-animate');
				}, k * 50, 'easeInOutExpo');
			});

		}, 100);

	}

}, {
	offset: '95%'
});
};
contentWayPoint();


 	// navigation
	var OnePageNav = function () {
		$(".smoothscroll[href^='#'], #ftco-nav ul li a[href^='#']").on('click', function (e) {
			e.preventDefault();

			var hash = this.hash,
				navToggler = $('.navbar-toggler');
			$('html, body').animate({
				scrollTop: $(hash).offset().top
			}, 700, 'easeInOutExpo', function () {
				window.location.hash = hash;
			});


			if (navToggler.is(':visible')) {
				navToggler.click();
			}
		});
		$('body').on('activate.bs.scrollspy', function () {
			console.log('nice');
		})
	};
	OnePageNav();


 	// magnific popup
		$('.image-popup').magnificPopup({
			type: 'image',
			closeOnContentClick: true,
			closeBtnInside: false,
			fixedContentPos: true,
			mainClass: 'mfp-no-margins mfp-with-zoom', // class to remove default margin from left and right side
			gallery: {
				enabled: true,
				navigateByImgClick: true,
				preload: [0, 1] // Will preload 0 - before current, and 1 after the current image
			},
			image: {
				verticalFit: true
			},
			zoom: {
				enabled: true,
				duration: 300 // don't foget to change the duration also in CSS
			}
		});

	$('.popup-youtube, .popup-vimeo, .popup-gmaps').magnificPopup({
		disableOn: 700,
		type: 'iframe',
		mainClass: 'mfp-fade',
		removalDelay: 160,
		preloader: false,
		fixedContentPos: false
	});
	// DatePicker
	$('.checkin_date').datepicker({
		dateFormat: 'dd/mm/yy',
		minDate: new Date(),	
		'autoclose': true
	});
	$('.checkout_date').datepicker({
		dateFormat: 'dd/mm/yy',
		minDate: $('.checkin_date').val() + 1 
	});

	$(".birth_date").datepicker({
		dateFormat: 'dd/mm/yy',
		'autoclose': true
	})
	//end of datepicker
	$( ".checkin_date" ).datepicker( "setDate", new Date() );
	$( ".checkout_date" ).datepicker( "setDate", $(".checkin_date").val() );
	$( ".checkout_date" ).datepicker( "option", "minDate", $(".checkin_date").val());

	$(document).on("change",".checkin_date",function(){
		$( ".checkout_date" ).datepicker( "option", "minDate", $(".checkin_date").val());
	})
	//dialog,form
		var dialog, form;
		$("#register").dialog({
		autoOpen: false,
		height: 600,
		width: 800,
		modal: true,
		closeOnEscape: true,
		buttons: {
			"Create an account": addUser,
			Cancel: function () {
				$(this).dialog("close");
			}
		},
		close: function () {
			$(this).dialog("close");
		}
		});
		function addUser() {
		$("#register").dialog("close");
		alert("Succesful");
	}
	dialog = $("#dialog").dialog({
		autoOpen: false,
		height: 270,	
		width: 320,
		maxHeight: 350,
		maxWidth: 320,
		resizable: false,
		modal: true,
		dialogClass: 'myTitleClass',
		position: 'center',	
		close: function () {
		//form[0].reset();
		$(this).dialog("close")

		}
	});
	$("#opener").on("click", function () {
		$("#dialog").dialog("open");
	});
	$("#open-register").on("click",function(){
		$("#register").dialog("open");
		$("#dialog").dialog("close");
	})
	$("#dialog").dialog("option", "closeOnEscape", true);
	$(".info-show").on("click", function () {
		$(".info-show").css("display", "none");
		$(".info-hide").css("display", "inline-block");
		$(".fare__details").css("display", "block");
	});
	$(".info-hide").on("click", function () {
		$(".info-hide").css("display", "none");
		$(".info-show").css("display", "inline-block");
		$(".fare__details").css("display", "none");
	})
	//end of dialog
	$(window).resize(function () {
		$("#dialog").dialog("option", "position", {
			my: "center",
			at: "center",
			of: window
		});
		$("#register").dialog("option", "position", {
			my: "center",
			at: "center",
			of: window
		});
		});
		
 	//Autocomplete
	$("#From").autocomplete({
		source: function(request,response){
			$.ajax({
				url: '/Index?handler=AirPort',
				datatype: "json",

				data:{term: request.term},
				contentType: "application/json; charset=utf-8",
				success: function(data){
					response($.map(data,function(item){
						return {
							label: item.name,
							value: item.name,
							CategoryId: item.id
						}
					}))
				},
				error: function(response){
					
				}
			});
		},
		minLength: 1,
		select: function(e,i)
		{
			$("#AirportId").val(i.item.CategoryId);
			//bind airport dropdown
			
		},
		
	}).focus(function(){
		$(this).autocomplete("search");
	});
	$("#Where").autocomplete({
		source: function(request,response){
			$.ajax({
				url: '/Index?handler=AirPort',
				datatype: "json",
				data:{term: request.term},
				contentType: "application/json; charset=utf-8",
				success: function(data){
					response($.map(data,function(item){
						return {
							label: item.name,
							value: item.name,
							CategoryId: item.id
						}
					}))
				},
				error: function(response){
					
				}
			});
		},
		minLength: 1,
		select: function(e,i)
		{
			$("#AirportId_2").val(i.item.CategoryId);
			//bind airport dropdown
			
		},
		
	}).focus(function(){
		$(this).autocomplete("search");
	});
	// End autocomplete
	//click everything
	$("#profile").on("click",function(){
		location.replace("/Account");
	})
	
	//Open close class
	$(".open-account").on("click",function(){
		var id = $(this).attr("id");
		if(id == "open-user"){
			$(".user-show").addClass("hidden");
			$(".user-field").removeClass("hidden");
			$("#open-user").addClass("hidden");
			$("#close-user").removeClass("hidden");
			$("#save-user").removeClass("hidden");
		}
		if(id == "open-pass")
		{
			$("#pass-field").removeClass("hidden");
			$("#pass-show").addClass("hidden");
			$("#open-pass").addClass("hidden");
			$("#close-pass").removeClass("hidden");
			$("#save-pass").removeClass("hidden");
		}
	});
	$(".close-account").on("click",function(){
		var id = $(this).attr("id");
		if(id == "close-user"){
			$(".user-show").removeClass("hidden");
			$(".user-field").addClass("hidden");
			$("#open-user").removeClass("hidden");
			$("#close-user").addClass("hidden");
			$("#save-user").addClass("hidden");
		}
		if(id == "close-pass")
		{
			$("#pass-field").addClass("hidden");
			$("#pass-show").removeClass("hidedn");
			$("#open-pass").removeClass("hidden");
			$("#close-pass").addClass("hidden");
			$("#save-pass").addClass("hidden");
		}
	});
	$(".open").on("click",function(){
		var id = $(this).attr("id");
		if(id == "profile_op")
		{
			$("#profile_op").addClass("hidden");
			$("#profile_cl").removeClass("hidden");
			$("#profile_s").removeClass("hidden");
			$(".pro-show").addClass("hidden");
			$(".pro-field").removeClass("hidden");
		}
		if(id == "add_op")
		{
			$("#add_op").addClass("hidden");
			$("#add_cl").removeClass("hidden");
			$("#add_s").removeClass("hidden");
			$("#address-show").addClass("hidden");
			$("#address-field").removeClass("hidden");
		}
		if(id == "phone_op")
		{
			$("#phone_op").addClass("hidden");
			$("#phone_cl").removeClass("hidden");
			$("#phone_s").removeClass("hidden");
			$(".phone-show").addClass("hidden");
			$(".phone-field").removeClass("hidden");
		}
		if(id == "email_op")
		{
			$("#email_op").addClass("hidden");
			$("#email_cl").removeClass("hidden");
			$("#email_s").removeClass("hidden");
			$(".email-show").addClass("hidden");
			$(".email-field").removeClass("hidden");
		}
	});
	$(".cancel").on("click",function(){
		var id = $(this).attr("id");
		if(id == "profile_cl")
		{
			$("#profile_op").removeClass("hidden");
			$("#profile_cl").addClass("hidden");
			$("#profile_s").addClass("hidden");
			$(".pro-show").removeClass("hidden");
			$(".pro-field").addClass("hidden");
		}
		if(id == "add_cl")
		{
			$("#add_op").removeClass("hidden");
			$("#add_cl").addClass("hidden");
			$("#add_s").addClass("hidden");
			$("#address-show").removeClass("hidden");
			$("#address-field").addClass("hidden");
		}
		if(id == "phone_cl")
		{
			$("#phone_op").removeClass("hidden");
			$("#phone_cl").addClass("hidden");
			$("#phone_s").addClass("hidden");
			$(".phone-show").removeClass("hidden");
			$(".phone-field").addClass("hidden");
		}
		if(id == "email_cl")
		{
			$("#email_op").removeClass("hidden");
			$("#email_cl").addClass("hidden");
			$("#email_s").addClass("hidden");
			$(".email-show").removeClass("hidden");
			$(".email-field").addClass("hidden");
		}
	});
	//end of open close class

	// select country
	var country_list = ["Afghanistan","Albania","Algeria","Andorra","Angola","Anguilla","Antigua &amp; Barbuda","Argentina","Armenia","Aruba","Australia","Austria","Azerbaijan","Bahamas","Bahrain","Bangladesh","Barbados","Belarus","Belgium","Belize","Benin","Bermuda","Bhutan","Bolivia","Bosnia &amp; Herzegovina","Botswana","Brazil","British Virgin Islands","Brunei","Bulgaria","Burkina Faso","Burundi","Cambodia","Cameroon","Cape Verde","Cayman Islands","Chad","Chile","China","Colombia","Congo","Cook Islands","Costa Rica","Cote D Ivoire","Croatia","Cruise Ship","Cuba","Cyprus","Czech Republic","Denmark","Djibouti","Dominica","Dominican Republic","Ecuador","Egypt","El Salvador","Equatorial Guinea","Estonia","Ethiopia","Falkland Islands","Faroe Islands","Fiji","Finland","France","French Polynesia","French West Indies","Gabon","Gambia","Georgia","Germany","Ghana","Gibraltar","Greece","Greenland","Grenada","Guam","Guatemala","Guernsey","Guinea","Guinea Bissau","Guyana","Haiti","Honduras","Hong Kong","Hungary","Iceland","India","Indonesia","Iran","Iraq","Ireland","Isle of Man","Israel","Italy","Jamaica","Japan","Jersey","Jordan","Kazakhstan","Kenya","Kuwait","Kyrgyz Republic","Laos","Latvia","Lebanon","Lesotho","Liberia","Libya","Liechtenstein","Lithuania","Luxembourg","Macau","Macedonia","Madagascar","Malawi","Malaysia","Maldives","Mali","Malta","Mauritania","Mauritius","Mexico","Moldova","Monaco","Mongolia","Montenegro","Montserrat","Morocco","Mozambique","Namibia","Nepal","Netherlands","Netherlands Antilles","New Caledonia","New Zealand","Nicaragua","Niger","Nigeria","Norway","Oman","Pakistan","Palestine","Panama","Papua New Guinea","Paraguay","Peru","Philippines","Poland","Portugal","Puerto Rico","Qatar","Reunion","Romania","Russia","Rwanda","Saint Pierre &amp; Miquelon","Samoa","San Marino","Satellite","Saudi Arabia","Senegal","Serbia","Seychelles","Sierra Leone","Singapore","Slovakia","Slovenia","South Africa","South Korea","Spain","Sri Lanka","St Kitts &amp; Nevis","St Lucia","St Vincent","St. Lucia","Sudan","Suriname","Swaziland","Sweden","Switzerland","Syria","Taiwan","Tajikistan","Tanzania","Thailand","Timor L'Este","Togo","Tonga","Trinidad &amp; Tobago","Tunisia","Turkey","Turkmenistan","Turks &amp; Caicos","Uganda","Ukraine","United Arab Emirates","United Kingdom","Uruguay","Uzbekistan","Venezuela","Vietnam","Virgin Islands (US)","Yemen","Zambia","Zimbabwe"];
	var option = '';
	var country = $("#country").val();
	if(country != null)
	{
		option += '<option value="' + country + '" selected>' + country + '</option>';
	}
	else
	{
		option += '<option value="0" selected>Select your country</option>';
	}
	for(var i = 0;i<country_list.length;i++)
	{
		option += '<option value="' + country_list[i] + '">' + country_list[i] + '</option>';
	}
	$("#items").append(option);
	//end of array coutries
	
	//test
	var firstName = $("#firstName").val();
	var lastName = $("#lastName").val();
	$(".name").text(firstName+" "+lastName);
	var birth_date = new Date() ;
	// $(".birth_day").text(birth_date);
	var OldPass;
	var phone ;
	var num;
	var street;
	var district;
	var state;
	var city;
	var email;
	
	// save profile
	$(".save").on("click",function(){
		var id = $(this).attr("id");
		firstName = $("#firstName").val();
		lastName =$("#lastName").val();
		birth_date = $(".birth_date").val();
		num =$("#num").val();
		street=$("#street").val();
		district = $("#district").val();
		state = $("#state").val();
		city = $("#city").val();
		country = $(".country").children("option:selected").val();
		phone = $("#phone").val();
		email = $("#email").val();
		var save_button = $(this).find("button");
		save_button.button("loading");
		$.ajax({
			type: "POST",
			url: "/Account?handler=EditCustomer",
			headers: {
			"XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
			},
			data: JSON.stringify({
				FirstName: firstName,
				LastName: lastName,
				Birthdate: birth_date,
				Num: num,
				Street: street,
				District: district,
				State: state,
				City: city,
				Country: country,
				Phone: phone,
				Email: email
			}),
			success:function(response){
				alert(response);
				$(".name").text(firstName+" "+lastName);
				$(".birth_day").text(birth_date);
				$("#text-num").text(num);
				$("#text-street").text(street);
				$("#text-district").text(district);
				$("#text-city").text(city);
				$("#text-state").text(state);
				$("#text-country").text(country);
				$("#text-phone").text(phone);
				$("#text-email").text(email);
				save_button.button("reset");
				if(id == "profile_s")
				{
					$("#profile_op").removeClass("hidden");
					$("#profile_cl").addClass("hidden");
					$("#profile_s").addClass("hidden");
					$(".pro-show").removeClass("hidden");
					$(".pro-field").addClass("hidden");
				}
				if(id == "add_s")
				{
					$("#add_op").removeClass("hidden");
					$("#add_cl").addClass("hidden");
					$("#add_s").addClass("hidden");
					$("#address-show").removeClass("hidden");
					$("#address-field").addClass("hidden");
				}
				if(id == "phone_s")
				{
					$("#phone_op").removeClass("hidden");
					$("#phone_cl").addClass("hidden");
					$("#phone_s").addClass("hidden");
					$(".phone-show").removeClass("hidden");
					$(".phone-field").addClass("hidden");
				}
				if(id == "email_s")
				{
					$("#email_op").removeClass("hidden");
					$("#email_cl").addClass("hidden");
					$("#email_s").addClass("hidden");
					$(".email-show").removeClass("hidden");
					$(".email-field").addClass("hidden");
				}
			},
			error: function(response){
				alert("Cant save your changes");
			}
		});
		// alert(num);
		
		
	});
	$(".save-account").on("click",function(){
		var id = $(this).attr("id");
		var UserName = $("#Username").val();
		var NewPass = $("#new-pass").val();
		var RePass = $("#re-pass").val();
		OldPass = $("#old-pass").val();
		var PassWordRegexp = new RegExp(/^(?=[^a-z]*[a-z])(?=\D*\d)[^:&.~\s]{5,20}$/);
		var save_button = $(this).find("button");
		
		if(id == "save-pass"){
			if(!PassWordRegexp.test(NewPass))
			{
				$("#err-new-pass").removeClass("hidden");
			}
			else{
				$("#err-new-pass").addClass("hidden");
				if(RePass != NewPass)
				{
					$("#err-re-pass").removeClass("hidden");
				}
				else{
					save_button.button('loading');
					$("#err-re-pass").addClass("hidden");
					$.ajax({
						type: "POST",
						url: "/Account?handler=EditAccountCustomer",
						headers: {
						"XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
						},
						data: JSON.stringify({
							// Username: UserName,
							Password: NewPass
						}),
						success: function(response)
						{
								$("#pass-field").addClass("hidden");
								$("#pass-show").removeClass("hidedn");
								$("#open-pass").removeClass("hidden");
								$("#close-pass").addClass("hidden");
								$("#save-pass").addClass("hidden");
								$("#new-pass").val("");
								$("#re-pass").val("");
								$("#old-pass").val(NewPass);
								save_button.button("reset");
						},
						error:function()
						{
							alert("false");
						}
					});	
				}
			}
		}
	})
	// end of save profile
})(jQuery);