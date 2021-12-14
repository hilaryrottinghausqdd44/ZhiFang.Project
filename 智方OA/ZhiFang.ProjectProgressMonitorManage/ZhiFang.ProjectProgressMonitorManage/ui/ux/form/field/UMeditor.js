/**
 * 简单在线编辑器:可用于交流,简单描述
 * @author longfc
 * @version 2017-03-20
 */
Ext.define('Shell.ux.form.field.UMeditor', {
	extend: 'Ext.form.field.TextArea',
	alias: ['widget.umeditor'],
	ueditorConfig: {},
	height: 80,
	margin: '2px 0px 2px 0px',
	emptyText: '等待输入...',
	fieldSubTpl: [
		'<textarea id="{id}" {inputAttrTpl}',
		'<tpl if="name"> name="{name}"</tpl>',
		'<tpl if="rows"> rows="{rows}" </tpl>',
		'<tpl if="cols"> cols="{cols}" </tpl>',
		'<tpl if="placeholder"> placeholder="{placeholder}"</tpl>',
		'<tpl if="size"> size="{size}"</tpl>',
		'<tpl if="maxLength !== undefined"> maxlength="{maxLength}"</tpl>',
		'<tpl if="readOnly"> readonly="readonly"</tpl>',
		'<tpl if="disabled"> disabled="disabled"</tpl>',
		'<tpl if="tabIdx"> tabIndex="{tabIdx}"</tpl>',
		'<tpl if="fieldStyle"> style="{fieldStyle}"</tpl>',
		' autocomplete="off">\n',
		'<tpl if="value">{[Ext.util.Format.htmlEncode(values.value)]}</tpl>',
		'</textarea>', {
			disableFormats: true
		}
	],
	toolbars: [
		[
			'bold', 'italic', 'underline', 'fontborder', 'strikethrough', 'superscript', 'subscript', '|',
			'forecolor', 'backcolor', '|',
			'fontfamily', 'fontsize', '|',
			'simpleupload', 'insertimage', 'emotion', 'scrawl', 'attachment', '|',
			'horizontal', 'date', 'time', 'spechars', 'snapscreen', 'wordimage'
		]
	],
	initComponent: function() {
		var me = this;
		me.ueditorConfig = {
			initialFrameHeight: me.height,
			initialFrameWidth: '100%',
			//是否启用元素路径，原编辑器默认是显示
			elementPathEnabled: false,
			//启用自动保存
		    enableAutoSave: false,
			//浮动时工具栏距离浏览器顶部的高度，用于某些具有固定头部的页面
			topOffset: 25,
			toolbarTopOffset: 30
		};
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initueditor();
		me.on({
			resize: function(form, width, height, oldWidth, oldHeight, eOpts) {
				me.setHeight(height);
			}
		});
	},
	initueditor: function(value) {
		var me = this;
		var testue = me.ue;
		if(!me.ue) {
			me.ueditorConfig = me.ueditorConfig || {};
			me.ueditorConfig.toolbars = me.toolbars;
			me.ue = UE.getEditor(me.getInputId(), me.ueditorConfig);
			me.ue.ready(function() {
				me.UEditorIsReady = true;
			});
			//这块 组件的父容器关闭的时候 需要销毁编辑器 否则第二次渲染的时候会出问题 可根据具体布局调整
			var win = me.up('window');
			if(win && win.closeAction == "hide") {
				win.on('beforehide', function() {
					me.onDestroy();
				});
			} else {
				var panel = me.up('panel');
				if(panel && panel.closeAction == "hide") {
					panel.on('beforehide', function() {
						me.onDestroy();
					});
				}
			}
		} else {
			me.ue.setContent(me.getValue());
		}
	},
	setHeight: function(height) {
		var me = this;
		if(height) height = height < 80 ? 80 : height;
		if(me.ue && me.ue.iframe && me.ue.iframe.parentNode) {
			var toolbarHeight = 20;
			var toolbar = me.ue.ui.toolbars[0];
			var dom = null;
			if(toolbar) dom = toolbar.getDom();
			if(dom && dom != null) toolbarHeight = dom.clientHeight;
			height = height - toolbarHeight;
			if(height > 0) me.ue.setHeight(height);
		}
	},
	setValue: function(value) {
		var me = this;
		if(value == undefined) {
			value = "";
		}
		if(!me.ue) {
			me.setRawValue(me.valueToRaw(value));
		} else {
			me.ue.ready(function() {
				me.ue.setContent(value);
			});
		}
		me.callParent(arguments);
		return me.mixins.field.setValue.call(me, value);
	},
	getRawValue: function() {
		var me = this;
		if(me.UEditorIsReady) {
			if(me.ue.sync != null)
				me.ue.sync(me.getInputId());
		}
		v = (me.inputEl ? me.inputEl.getValue() : Ext.value(me.rawValue, ''));
		me.rawValue = v;
		return v;
	},
	destroyUEditor: function() {
		var me = this;
		if(me.rendered) {
			try {
				me.ue.destroy();
				var dom = document.getElementById(me.id);
				if(dom) {
					dom.parentNode.removeChild(dom);
				}
				me.ue = null;
			} catch(e) {}
		}
	},
	onDestroy: function() {
		var me = this;
		me.callParent();
		me.destroyUEditor();
	}
});