$(document).ready(function() {
	
	$('.shapes-radios input[type=radio]:checked').next('label').addClass('checked');
	
	$('.shapes-checkboxes input[type=checkbox]:checked').next('label').addClass('checked');
	
	$('.shapes-checkboxes label').click(function(){
		$(this).toggleClass('checked');
	})
	
	$('.shapes-radios label').click(function(){
		$('.shapes-radios label').removeClass('checked');
		$(this).addClass('checked');
	})

	var selectBox = '<div class="select-box">\
		<p class="selected-option"></p>\
		<ul class="options">';

	$('select.shapes-select option').each(function(){
		selectBox = selectBox + '<li>' + $(this).text() +'</li>';
	})
		
	selectBox = selectBox + '</ul></div>';
	$(selectBox).insertAfter('select.shapes-select');
		
	$('.selected-option').text($('select.shapes-select option:selected').text());


	$('.select-box .selected-option').click(function(e){
		e.stopPropagation();
		$('.select-box').toggleClass('open');
	})
	
	$('.select-box .options li').click(function(){
		var optText = $(this).text();
		
		$('.select-box .options li').removeClass('active');
		$(this).addClass('active');
		
		$('.select-box').removeClass('open')
		.find('.selected-option')
		.text(optText);
		
		$('select.shapes-select option').eq($(this).index()).prop('selected', true);	
	})
	
	$('body').click(function(e){
	
		if($('.select-box').hasClass('open')){
			$('.select-box').removeClass('open');
		}
	   
	});
	
});