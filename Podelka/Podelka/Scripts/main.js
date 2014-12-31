$(function() {
   $('#findButton').click(function( e ) {
		e.preventDefault();
		
    	$( '#findButton' ).css({  
		position:"absolute", 
		right:"0px" 
		
		});
		
		$(".findLine").css("display","block");
   });
});

$(window).on('load , resize', function createSize(){
	doc_w = $(document).width();
	doc_h = $(document).height();

	cont_w = $('.container').width();
	cont_h = $('.container').height();
	$('.container').css({'top':(doc_h-cont_h)/2});
	$('.container').css({'left':(doc_w-cont_w)/2});
	$('.container').css({'opacity':1});
});
