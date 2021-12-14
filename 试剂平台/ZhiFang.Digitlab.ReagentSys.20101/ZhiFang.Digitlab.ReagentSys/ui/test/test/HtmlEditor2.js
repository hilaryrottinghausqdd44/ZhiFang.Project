Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.HtmlEditor',{
	extend:'Ext.form.HtmlEditor',
	alias:'widget.zhifangux_htmleditor',
    addImage: function() {
        var editor = this;
        var imgform = new Ext.FormPanel({
            region: 'center',
            labelWidth: 55,
            bodyStyle: 'padding:20px 5px 0',
            autoScroll: true,
            border: false,
            buttonAlign: 'center',
            fileUpload: true,
            items: [{
                xtype: 'textfield',
                fieldLabel: '选择文件',
                name: 'Pic',
                inputType: 'file',
                allowBlank: false,
                blankText: '文件不能为空',
                height: 25,
                anchor: '90%'

			}],
            buttons: [{
                text: '插入',
                type: 'submit',
                handler: function(){
                    if (!imgform.form.isValid()) {
                        return;
                    }
                    var photo = imgform.form.items.items[0].getValue();
                    var fileext = photo.substring(photo.lastIndexOf("."), photo.length).toLowerCase();
                    if (fileext != '.jpg' && fileext != '.gif' && fileext != '.jpeg' && fileext != '.png' && fileext != '.bmp') {
                        imgform.form.items.items[0].setValue('');
                        Ext.Msg.show({
                            title: '提示',
                            icon: 'ext-mb-error ',
                            width: 300,
                            msg: '对不起，系统仅支持标准格式的照片，请您调整格式后重新上传，谢谢 ！',
                            buttons: Ext.MessageBox.OK
                        });
                        return;
                    }
                    imgform.form.submit({
                        waitMsg: '图片正在插入..',
                        url: editor.url + "?sign=HTMLEditor", //点击插入执行的方法,将图片保存到服务器上
                        success: function(form, action){
                            var element = document.createElement("img");
                            element.src = action.result.data; //action.result.data "\HtmlEditorPics\abc.jpg （gsr.data）
                            if(Ext.isIE){
                                editor.insertAtCursor(element.outerHTML);
                            }else{
                                var selection = editor.win.getSelection();
                                if (!selection.isCollapsed){
                                    selection.deleteFromDocument();
                                }
                                selection.getRangeAt(0).insertNode(element);

                            } //element <img src="\HtmlEditorPics\abc.jpg"/>
                            win.hide();
                        },
                        failure: function(form, action) {
                            form.reset();
                            if (action.failureType == Ext.form.Action.SERVER_INVALID)
                                Ext.MessageBox.alert('警告', action.result.errors.msg);
                        }
                    });
                }
            },{
                text: '取消',
                type: 'submit',
                handler: function(){
                    win.hide();
                }
			}]
		});
            var win = new Ext.Window({
            title: "插入图片",
            width: 400,
            height: 150,
            plain: true,
            modal: true,
            closeAction: 'hide',
            border: false,
            layout: "fit",
            items: imgform
        });
        win.show(this);
    },
    createToolbar: function(editor){
        this.superclass.createToolbar.call(this, editor);
        this.tb.insertButton(16,{
            cls: "x-btn-icon",
            iconCls: "ico_insertPicture",
            handler: this.addImage,
            scope: this
        });
    }
});
