/**
 * 颜色选择
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.alertInfo.ColorField', {
    extend: 'Ext.form.field.Trigger',
    alias: 'widget.colorfield',
	
    triggerTip: '请选择一个颜色',
	
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
    onTriggerClick: function() {
        var me = this;
        if (!me.picker) {
			me.picker = Ext.create('Ext.picker.Color', {
			    pickerField: this,
			    ownerCt: this,
			    renderTo: Ext.getBody(),
			    floating: true,
			    name: 'Color',
			    itemId: 'Color',
			    focusOnShow: true,
			    style: {
			      backgroundColor: "#fff"
			    },
			    listeners: {
			      scope: this,
			      select:function(field, value, opts) {
			        me.setValue('#' + value);
			        
			        me.inputEl.applyStyles({
			            backgroundColor: '#' + value
			        });
			        me.setFieldStyle('background:#' + value + ';color:#FFFFFF');

			        me.picker.hide();
			      }
			    }
	        });
	        me.picker.alignTo(me.inputEl, 'tl-bl?');
	    }
	    me.picker.show();
	    var attached = me.attached || false;
	    me.lastShow = new Date();
	    if (!attached) {
	      Ext.getDoc().on('mousedown', me.onMouseDown, me, {
	          buffer: Ext.isIE9m ? 10 : undefined
	      });
	      me.attached = true;
	    }
    },
    onMouseDown: function(e) {
	    var lastShow = this.lastShow,
	      doHide = true;
		    if (Ext.Date.getElapsed(lastShow) > 50 && !e.getTarget('#' + this.picker.id)) {
		      if (Ext.isIE9m && !Ext.getDoc().contains(e.target)) {
		        doHide = false;
		      }
		      if (doHide) {
		        this.picker.hide();
		        Ext.getDoc().un('mousedown', this.onMouseDown, this);
		        this.attached = false;
		    }
	    }
    }
  
});