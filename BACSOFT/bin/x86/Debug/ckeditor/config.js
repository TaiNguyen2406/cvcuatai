/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    config.language = 'vi';
    //config.uiColor = '#AADC6E';
    config.skin = 'office2013';
    config.removePlugins = 'elementspath';
    config.resize_enabled = false;
    config.extraPlugins = 'popup,lineheight,tableresize,textselection,sourcedialog,tenkhachhang,cachxungho,wysiwygarea,zoom,filebrowser';

    config.enterMode = CKEDITOR.ENTER_BR;
    config.toolbarCanCollapse = true;
    config.tabSpaces = 4;
    config.shiftEnterMode = CKEDITOR.ENTER_P;


    config.htmlEncodeOutput = true;


  



    config.line_height = "8px;9px;10px;11px;12px;13px;14px;16px;18px;21px;22px;24px;26px;28px;30px;31px;36px;38px;40px;42px;46px;48px;72px;100px;";


	
	


};


