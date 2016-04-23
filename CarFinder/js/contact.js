$(document).ready(function() {
	$('#contact-form').submit(function() {
		
		var buttonCopy = $('#contact-form button').html(),
			errorMessage = $('#contact-form button').data('error-message'),
			sendingMessage = $('#contact-form button').data('sending-message'),
			okMessage = $('#contact-form button').data('ok-message'),
			hasError = false;
		
		$('#contact-form .error-message').remove();
		
		$('.requiredField').each(function() {
			if($.trim($(this).val()) == '') {
				var errorText = $(this).data('error-empty');
				$(this).parent().append('<span class="error-message" style="display:none;">'+errorText+'.</span>').find('.error-message').fadeIn('fast');
				$(this).addClass('inputError');
				hasError = true;
			} else if($(this).is("input[type='email']") || $(this).attr('name')==='email') {
				var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
				if(!emailReg.test($.trim($(this).val()))) {
					var invalidEmail = $(this).data('error-invalid');
					$(this).parent().append('<span class="error-message" style="display:none;">'+invalidEmail+'.</span>').find('.error-message').fadeIn('fast');
					$(this).addClass('inputError');
					hasError = true;
				}
			}
		});
		
		/*Custom*/
		if($('.shapes-checkboxes').is('.required') && !$('input[type=checkbox]:checked').val()){
			var errorText = $('.shapes-checkboxes').data('error-text');
			$('.shapes-checkboxes').append('<span class="error-message" style="display:none;">'+errorText+'.</span>').find('.error-message').fadeIn('fast');
			hasError = true;
		}
		
		if($('.shapes-radios').is('.required') && !$('input[type=radio]:checked').val()){
			var errorText = $('.shapes-radios').data('error-text');
			$('.shapes-radios').append('<span class="error-message" style="display:none;">'+errorText+'.</span>').find('.error-message').fadeIn('fast');
			hasError = true;
		}
		
		if($('.shapes-select').is('.required') && $.trim($('.shapes-select').val())==''){
			var errorText = $('.shapes-select').data('error-text');
			$('.select-box').append('<span class="error-message" style="display:none;">'+errorText+'.</span>').find('.error-message').fadeIn('fast');
			hasError = true;
		}
		
		/*-----*/
		if(hasError) {
			$('#contact-form button').html('<i class="fa fa-times"></i>'+errorMessage);
			setTimeout(function(){
				$('#contact-form button').html(buttonCopy);
			},2000);
		}
		else {
			$('#contact-form button').html('<i class="fa fa-spinner fa-spin"></i>'+sendingMessage);
			
			var formInput = $(this).serialize();
			$.post($(this).attr('action'),formInput, function(data){
				$('#contact-form button').html('<i class="fa fa-check"></i>'+okMessage);
				setTimeout(function(){
					$('#contact-form button').html(buttonCopy);
				},2000);
				
			});
		}
		
		return false;	
	});
});