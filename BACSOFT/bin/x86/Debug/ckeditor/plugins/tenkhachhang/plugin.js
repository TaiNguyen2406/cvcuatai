
CKEDITOR.plugins.add( 'tenkhachhang', {
    icons: 'tenkhachhang',
    init: function( editor ) {
        editor.addCommand( 'inserttenkhachhang', {
            exec: function( editor ) {
                var now = new Date();
                editor.insertHtml( '[#{TEN_KH}#]' );
            }
        });
        editor.ui.addButton( 'tenkhachhang', {
            label: 'Chèn tên biến khách hàng',
            command: 'inserttenkhachhang',
            toolbar: 'insert'
        });
    }
});

