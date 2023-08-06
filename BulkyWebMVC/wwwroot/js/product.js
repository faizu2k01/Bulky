var dataTable;


$(document).ready(function () {
    loadDataTables();
})


function loadDataTables(){
      dataTable =  $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },
        "columns": [
            { data: 'title', "width": '15%' },
            { data: 'isbn', "width": '10%' },
            { data: 'author', "width": '15%' },
            { data: 'category.name', "width":'15%' },
              { data: 'id',

                  "render": function (data) {
                      return ` <div class="w-50 btn-group mx-lg-5" role="group">
                                    <a class="mx-2"  href="/admin/product/upsert?id=${data}">
                                        <i class="bi bi-pencil-square"></i>
                                    </a>
                                    <a class="mx-2" onClick=Delete('/admin/product/delete?id=${data}') >
                                        <i class="bi bi-trash-fill"></i>
                                    </a>
                                </div>`
                  }
              , "width": '15%' },
            
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}