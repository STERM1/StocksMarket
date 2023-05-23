
$(function () {
    var dtArray = ['txtContractStartDate', 'txtExpiry'];
    var i = 0;
    for (i = 0; i < dtArray.length; i++) {
        //$('#txtDOJ').datepicker();
        $('#'+dtArray[i]).datepicker();
        }               
});



   
function HideUnhideRel() {
    $('#rdRelYes').change(function () {
        $("#lblRelative").removeClass("file-hide")
        // $("#divreldet").addClass("");
    });
}

function checkFileType(input) {

    if (input.files && input.files[0]) {
        var maxFileSize = 4096000 // 4MB -> 4000 * 1024
        if (input.files[0].size < maxFileSize) {
            if (input.files[0].type == "image/jpeg" || input.files[0].type == "image/png" || input.files[0].type == ".doc" || input.files[0].type == "application/pdf") {
                $("#warning").hide();
                return true;
            }
            else {
                $("#warning").text('Only .pdf/.doc/.jepg/.png extensions files are allowed');
                $("#warning").show();
                return false;
            }
        }
        else {
            alert('You can not upload file size of more than 4MB')
            $("#warning").text('You can not upload file size of more than 4MB');
            $("#warning").show();
            return false;

        }
    }       
}
    
$(function () {
    $('#chkPicUpload').change(function () {
        $("#picFile").toggle(this.checked);
    }).change();
});

$(function () {
    $('#cb1').change(function () {
        $("#file1").toggle(this.checked);
    }).change();
});

$(function () {
    $('#cb2').change(function () {
        $("#file2").toggle(this.checked);
    }).change();
});
$(function () {
    $('#cb3').change(function () {
        $("#file3").toggle(this.checked);
    }).change();
});
$(function () {
    $('#cb4').change(function () {
        $("#file4").toggle(this.checked);
    }).change();
});
$(function () {
    $('#cb5').change(function () {
        $("#file5").toggle(this.checked);
    }).change();
});
$(function () {
    $('#cb6').change(function () {
        $("#file6").toggle(this.checked);
    }).change();
});
$(function () {
    $('#cb7').change(function () {
        $("#file7").toggle(this.checked);
    }).change();
});
$(function () {
    $('#cb8').change(function () {
        $("#file8").toggle(this.checked);
    }).change();
});
$(function () {
    $('#cb9').change(function () {
        $("#file9").toggle(this.checked);
    }).change();
});
$(function () {
    $('#cb10').change(function () {
        $("#file10").toggle(this.checked);
    }).change();
});
$(function () {
    $('#cb11').change(function () {
        $("#file11").toggle(this.checked);
    }).change();
});
$(function () {
    $('#cb12').change(function () {
        $("#file12").toggle(this.checked);
    }).change();
});

function SameAddressRepeat()
    {
        if ($("#cb1").prop("checked") == true) {
            $("#txtPermanentAddress").text('test');
        }
    }

function CheckNomonation() {        
    document.getElementById("NomWarning").style.display = 'none';
    var confirm_Nom = document.createElement("INPUT");
    confirm_Nom.type = "hidden";
    confirm_Nom.name = "confirm_Nom";         
    if (isNaN($("#txtNom1Percent").val())) {
        $("#NomWarning").text('Please ensure the quantity specified is numeric for nominee percentage');
        $("#NomWarning").show();

        // alert("Please ensure the quantity specified is numeric for nominee percentage");
        $("#txtNom1Percent").val("1");
        confirm_Nom.value = "No";
        document.forms[0].appendChild(confirm_Nom);
                
        return false;
    }
    else{
        confirm_Nom.value = "Yes";
        document.forms[0].appendChild(confirm_Nom);
                
    }
    if (isNaN($("#txtNom2Percent").val())) {
        //alert("Please ensure the quantity specified is numeric for nominee percentage");
        $("#NomWarning").text('Please ensure the quantity specified is numeric for nominee percentage');
        $("#NomWarning").show();

        $("#txtNom2Percent").val("1");
        confirm_Nom.value = "No";
        document.forms[0].appendChild(confirm_Nom);
        return false;
    }
    else {
        confirm_Nom.value = "Yes";
        document.forms[0].appendChild(confirm_Nom);
                
    }
    if (isNaN($("#txtNom3Percent").val())) {
        //alert("Please ensure the quantity specified is numeric for nominee percentage");
        $("#NomWarning").text('Please ensure the quantity specified is numeric for nominee percentage');
        $("#NomWarning").show();

        $("#txtNom2Percent").val("1");
        confirm_Nom.value = "No";
        document.forms[0].appendChild(confirm_Nom);
        return false;
    }
    else {
        confirm_Nom.value = "Yes";
        document.forms[0].appendChild(confirm_Nom);
        return true;
    }

            

        
}     

function SelfDeclaration() {
    document.getElementById("warning").style.display = 'none';
    var confirm_value = document.createElement("INPUT");
    confirm_value.type = "hidden";
    confirm_value.name = "confirm_save";

    if ($("#cb1").prop("checked")== true)
    {
        if ($("#file1").val() == '') {
            $("#warning").text('Please upload marks sheet for SSC certificate');
            $("#warning").show();
            confirm_value.value = "No";
            document.forms[0].appendChild(confirm_value);
            return false;
        }
                
    } 
    if ($("#cb2").prop("checked")== true) {
        if ($("#file2").val() == '') {
            $("#warning").text('Please upload marks sheet for HSC certificate');
            $("#warning").show();
            confirm_value.value = "No";
            document.forms[0].appendChild(confirm_value);
            return false;
        }

    }
    if ($("#cb3").prop("checked")== true) {
        if ($("#file3").val() == '') {
            $("#warning").text('Please upload marks sheet for Graduation');
            $("#warning").show();
            confirm_value.value = "No";
            document.forms[0].appendChild(confirm_value);
            return false;
        }

    }
    if ($("#cb4").prop("checked")== true) {
        if ($("#file4").val() == '') {
            $("#warning").text('Please upload marks sheet for PGD');
            $("#warning").show();
            confirm_value.value = "No";
            document.forms[0].appendChild(confirm_value);
            return false;
        }

    }
    if ($("#cb5").prop("checked") == true) {
        if ($("#file5").val() == '') {
            $("#warning").text('Please upload Offer Letter / Appointment Letter of previous organization');
            $("#warning").show();
            confirm_value.value = "No";
            document.forms[0].appendChild(confirm_value);
            return false;
        }

    }
    if ($("#cb6").prop("checked") == true) {
        if ($("#file6").val() == '') {
            $("#warning").text('Please upload Offer Letter / Appointment Letter of previous to previous organization');
            $("#warning").show();
            confirm_value.value = "No";
            document.forms[0].appendChild(confirm_value);
            return false;
        }

    }
    if ($("#cb7").prop("checked") == true) {
        if ($("#file7").val() == '') {
            $("#warning").text('Please upload Last 3 Months Salary Slips of previous organization or Bank Statement');
            $("#warning").show();
            confirm_value.value = "No";
            document.forms[0].appendChild(confirm_value);
            return false;
        }

    }
    if ($("#cb8").prop("checked") == true) {
        if ($("#file8").val() == '') {
            $("#warning").text('Please upload Relieving Letter / Resignation Acceptance letter of previous 2 organizations');
            $("#warning").show();
            confirm_value.value = "No";
            document.forms[0].appendChild(confirm_value);
            return false;
        }

    }
    if ($("#cb9").prop("checked") == true) {
        if ($("#file9").val() == '') {
            $("#warning").text('Please upload photograph with red bg');
            $("#warning").show();
            confirm_value.value = "No";
            document.forms[0].appendChild(confirm_value);
            return false;
        }

    }
    if ($("#cb10").prop("checked") == true) {
        if ($("#file10").val() == '') {
            $("#warning").text('Please upload current address proof');
            $("#warning").show();
            confirm_value.value = "No";
            document.forms[0].appendChild(confirm_value);
            return false;
        }

    }
    if ($("#cb11").prop("checked") == true) {
        if ($("#file11").val() == '') {
            $("#warning").text('Please upload permanent address proof');
            $("#warning").show();
            confirm_value.value = "No";
            document.forms[0].appendChild(confirm_value);
            return false;
        }

    }
    if ($("#cb12").prop("checked") == true) {
        if ($("#file12").val() == '') {
            $("#warning").text('Please upload scan copy of PAN Card');
            $("#warning").show();
            confirm_value.value = "No";
            document.forms[0].appendChild(confirm_value);
            return false;
        }

    }


    if ($("[id*=txtName]").val() == '')
    {
        alert('name bank');
        $("#warning").text('Name can not be left blank');
        $("#warning").show();
        $("[id*=txtName]").focus();
        confirm_value.value = "No";
        document.forms[0].appendChild(confirm_value);
        return false;
    }
    if ($("[id*txtDesig]").val() == '')
    {
        $("#warning").text('Designation can not be left blank');
        $("#warning").show();
        $("[id*=txtDesig]").focus();
        confirm_value.value = "No";
        document.forms[0].appendChild(confirm_value);
        return false;
    }
    if ($("[id*txtDept]").val() == '') {
        $("#warning").text('Department  can not be left blank');
        $("#warning").show();
        $("[id*=txtDept]").focus();
        confirm_value.value = "No";
        document.forms[0].appendChild(confirm_value);
        return false;
    }
    if ($("[id*txtDOJ]").val() == '') {
        $("#warning").text('Date of Joining  can not be left blank');
        $("#warning").show();
        $("[id*=txtDOJ]").focus();
        confirm_value.value = "No";
        document.forms[0].appendChild(confirm_value);
        return false;
    }
    else {
        confirm_value.value = "Yes";
        document.forms[0].appendChild(confirm_value);

        var objData = [];
        var selectedCkCount = 0;
        var element;
        /* for (var i = 0; i < document.getElementsByName('radSapId').length ; i++) {
             if (document.getElementsByName('radSapId')[i].checked == true) {
                 var sapId = document.getElementsByName('radSapId')[i].id;
                 var appraiser = $('#appraiser_' + sapId).val();
                 var Details = { SapId: sapId, Name: $('#name_' + sapId).val(), Desig: $('#desig_' + sapId).val(), Dept: $('#dept_' + sapId).val(), Appraiser: $('#appraiser_' + sapId).val(), Hod: $('#hod_' + sapId).val(), Hrbp: $('#hrbp_' + sapId).val(), channel: $('#channle_' + sapId).val(), period: $('#period_' + sapId).val(), award_name: $('#award_name_' + sapId).val(), achievement: $('#txtAchievement').val(), bus_impact: $('#txtBusImpact').val(), award_id: $('#award_id_' + sapId).val() };
                 objData.push(Details);

             }

         }*/
        var name = $('#txtName').val();


        var ObjFinal = JSON.stringify(objData);
        $.ajax({
            type: "POST",
            url: "Default.aspx/SaveRecord",
            data: "{'data':'" + ObjFinal + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                alert(data.d);
                window.location.href = "Default.aspx";
            },
            failure: function (response) {
                alert(response);
            }

        });
    }
    document.forms[0].appendChild(confirm_value);

}

