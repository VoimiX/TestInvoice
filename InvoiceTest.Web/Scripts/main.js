$(document).ready(function () {

    $('body').on('click', 'a.linkedit', function (e) {
        $("#dialog").dialog("open");
        $('#accountid').val($(this).data('accountid'));
        $('#clientid').val($(this).data('clientid'));
    });


    $('#text-input').on('input', function (e) {

        $("#dialog").dialog('close');

        if ($('#text-input').val() == '') {
            $('#result-area').html('');
            return;
        }
        

        $.ajax({
            type: 'POST',
            data: { input: $('#text-input').val() },
            url: '/Invoice/Search',
            success: function (data) {
                $('#result-area').html(data);
            },
            error: function (data) {
                alert('Что-то пошло не так при поиске')
            }
        });

    });

   

    //Initialize dialog
    $("#dialog").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "blind",
            duration: 1000
        },
        width: "400px",

        open: function (event, ui) {
          
            $('#sum-input').val('');
        }
      
    });

    $('#btn-save').on('click', function () {
        var sum = $('#sum-input').val();

        var valid = IsNumeric(sum);
        if (!valid) {
            alert('Данные не соотвествуют числовому формату');
            return;
        }

        $.ajax({
            type: 'POST',
            data: { amount: sum, clientId: $('#clientid').val(), accountid: $('#accountid').val() },
            url: '/Invoice/Add',
            success: function (data) {

                var error = ''
                $.each(data, function (index, value) {
                    error += value + '\n';                 
                });

                if (error != '') {
                    alert('При сохранении произошли следующие ошибки:\n' + error)
                   

                } else {
                    $("#dialog").dialog('close');                   
                }
            },
            error: function (data) {
                alert('Что-то пошло не так при сохранении')
            }
        });


       
    });

    $('#btn-cancel').on('click', function () {
        $("#dialog").dialog('close');
    });

});


function IsNumeric(input) {
    return (input - 0) == input && ('' + input).trim().length > 0;
}