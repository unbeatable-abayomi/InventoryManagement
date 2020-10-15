var dataTable;


$(document).ready(function () {
	loadDataTable();
});



function loadDataTable() {
	dataTable = $("#tblData").DataTable({
		"ajax": {
			"url": "/Admin/WareHouse/GetAll"
		},
		"columns": [
			{ "data": "title", "width": "20%" },
			{ "data": "location", "width": "15%" },
			{ "data": "description", "width": "40%" },
			{
				"data": "id",
				"render": function (data) {
					return `
							<div class="text-center">
								<a href="/Admin/WareHouse/Upsert/${data}" class="btn btn-success text-white style="cursor:pointer"">
									<i class="fas fa-edit"></i>
								</a>
								<a onclick=Delete("/Admin/WareHouse/Delete/${data}") class="btn btn-danger text-white style="cursor:pointer"">
									<i class="fas fa-trash-alt"></i>
								</a>
							</div>
						   `;
				}, "width": "40%"
			}
		]
	});
}



function Delete(url) {
	swal({
		title: "Are you sure you want to delete this?",
		text: "You will not be Able to Restore the data!! ",
		icon: "warning",
		buttons: true,
		dangerMode: true
	}).then((willDelete) => {
		if (willDelete) {
			$.ajax({
				type: "DELETE",
				url: url,
				success: function (data) {
					if (data.success) {
						toastr.success(data.message);
						dataTable.ajax.reload();
					} else {
						toastr.error(data.message);
					}
				}
			})
		}
	});
}