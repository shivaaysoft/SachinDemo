$(document).ready(function () {
    $("#agreementDatatable").DataTable({
        "processing": true,
        "serverSide": true,
        "orderMulti": false,
        "filter": true,
        "ajax": {
            "url": "/Home/GetData",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            {
                data: "UserName",
                orderable: true,
                render: function (data, type, full, meta) {
                    var user = full.userName != null ? full.userName : "";
                    return "<span>" + user + "</span>";
                }
            },
            {
                data: "ProductGroupCode",
                orderable: true,
                render: function (data, type, full, meta) {
                    var productGroupCode = full.productGroupCode;
                    var groupDescription = full.groupDescription;
                    return "<span title='" + groupDescription + "'>" + productGroupCode + "</span>";
                }
            },
            {
                data: "ProductNumber",
                orderable: true,
                render: function (data, type, full, meta) {
                    var productNo = full.productNumber;
                    var productDescription = full.productDescription;
                    return "<span title='" + productDescription + "'>" + productNo + "</span>";
                }
            },
            {
                data: "EffectiveDate",
                orderable: true,
                render: function (data, type, full, meta) {
                    var value = moment(full.effectiveDate).format('MM/DD/YY');
                    return "<span>" + (full.effectiveDate != null ? value : "") + "</span>";
                }
            },
            {
                data: "ExpirationDate",
                orderable: true,
                render: function (data, type, full, meta) {
                    var value = moment(full.expirationDate).format('MM/DD/YY');
                    return "<span>" + (full.expirationDate != null ? value : "") + "</span>";
                }
            },
            {
                data: "ProductPrice",
                orderable: true,
                render: function (data, type, full, meta) {
                    return "<span>" + (full.productPrice != null ? full.productPrice : "") + "</span>";
                }
            },
            {
                data: "NewPrice",
                orderable: true,
                render: function (data, type, full, meta) {
                    return "<span>" + (full.newPrice != null ? full.newPrice : "") + "</span>";
                }
            },
            {
                orderable: false,
                render: function (data, type, full, meta) {
                    return "<a class='btn btn-info btn-sm' onclick=PopupForm("+full.id+")><i class='fa fa-pencil'></i> Edit</a><a class='btn btn-danger btn-sm' style='margin-left:5px' onclick=DeleteAgreement(" + full.id + ")><i class='fa fa-trash'></i> Delete</a>";
                }
            },
        ]
    });
});

function PopupForm(id) {
    AddEditAgreement(id)
}
function AddEditAgreement(id) {
    $.ajax({
        url: '/Home/AddEditAgreement',
        data: { id: id },
        type: 'GET',
        success: function (data) {
            $('#modelContent').html(data);
            $('#addEditModal').modal('show');
        }
    });
}
function DeleteAgreement(id) {
    if (confirm("Are you sure?")) {
        $.ajax({
            url: '/Home/DeleteAgreement',
            data: { id: id },
            type: 'POST',
            success: function (response) {
                if (response.response === true) {
                    $('#agreementDatatable').dataTable().fnDraw();
                }
            }
        })
    }
    return false;
}
function SubmitForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: 'POST',
            url: '/Home/AddEditAgreement',
            data: $(form).serialize(),
            dataType: "json",            
            success: function (response) {
                if (response.result = true) {
                    $('#agreementDatatable').dataTable().fnDraw();
                }
            }
        })
    }
    else {
        return false;
    }
}

function LoadProduct() {
    var productGroupId = $("#ProductGroupId").val();
    $.ajax({
        url: '/Home/GetProductsByProductGroupId',
        data: { productGroupId: productGroupId },
        type: 'GET',
        success: function (response) {
            $('#ProductId').empty();
            $('#ProductId').closest('select').append($('<option value="">-- Select Product --</option>'));
            for (var i = 0; i < response.productlist.length; i++) {
                $('#ProductId').closest('select').append($('<option value="' + response.productlist[i].value + '">' + response.productlist[i].text + '</option>'));
            }
            $('#ProductId').closest('select').trigger("chosen:updated");
        }
    });
}