
CKEDITOR.plugins.add( 'cachxungho', {
    icons: 'cachxungho',
    init: function( editor ) {
        editor.addCommand( 'insertcachxungho', {
            exec: function( editor ) {
                var now = new Date();
                editor.insertHtml( '[#{XUNG_HO}#]' );
            }
        });
        editor.ui.addButton( 'cachxungho', {
            label: 'Chèn cách xưng hô khách hàng',
            command: 'insertcachxungho',
            toolbar: 'insert'
        });
    }
});

