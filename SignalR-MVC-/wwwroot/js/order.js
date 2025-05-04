var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Home/GetAllOrder"
        },
        "columns": [
            { "data": "orderId", "width": "1rem" },
            { "data": "name", "width": "20rem" },
            { "data": "itemName", "width": "20rem" },
            { "data": "count", "width": "1rem" },
            {
                "data": "orderId",
                "render": function (data) {
                    return `
                            <div class="w-75 btn-group" role="group">
                            <a href=""
                            class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> </a>
					        </div>
                        `
                },
                "width": "5%"
            }
        ]
    });
}


var orderConnection = new signalR.HubConnectionBuilder()
    .withUrl("/orderHub").build();

orderConnection.on("ReceiveOrder", () => {
    dataTable.ajax.reload();
    toastr.success("New Order Received");
});

orderConnection.start();