/**
 * 选择文本框
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.form.field.CheckTrigger',{
    extend:'Ext.form.field.Trigger',
    alias:'widget.uxCheckTrigger',
	
	fieldLabel:'',
	triggerCls:'x-form-search-trigger',
	enableKeyEvents:false,
	editable:false,
	
	/**弹出的类名*/
	className:'',
	/**参数*/
	classConfig:{},
	
	/**是否窗口弹出模式*/
	isWinOpen:false,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		//支持双击input框弹出组件窗体
		me.inputEl.on({
			dblclick:function(p,data){
				me.onTriggerClick();
			}
		});
	},
    
	initComponent:function(){
		var me = this;
		
		me.addEvents('check','beforetriggerclick');
		
		me.callParent(arguments);
	},
	
	/**查询点击处理*/
	onTriggerClick: function() {
        var me = this;
        
        var bo = me.fireEvent('beforetriggerclick',me);
		if(bo === false) return;
        
        if(!me.className){
			JShell.Msg.warning('请配置className参数!');
			return;
		}
        
        if(me.isWinOpen){//窗口弹出模式
        	JShell.Win.open(me.className,me.classConfig).show();
        }else{//下拉框模式
        	if (!me.readOnly && !me.disabled) {
	            if (me.isExpanded) {
	                me.collapse();
	            } else {
	                me.expand();
	            }
	            me.inputEl.focus();
	        }
        }
    },
    /**创建picker*/
	createPicker: function() {
        var me = this,
            picker;
            
        if(!me.isWinOpen){
			me.classConfig.header = false;
		}
		
		me.classConfig.resizable = (me.classConfig.resizable === false ? false : true);
		
		if(me.classConfig.checkOne === false){
			me.classConfig.selModel = {mode:'SIMPLE'};
		}
		
        me.classConfig.listeners = me.classConfig.listeners || {};
        me.classConfig.listeners.accept = function(p,records){
        	me._checkedData = records;
        	me.collapse();
        	me.fireEvent('check',me,records);
		};
		me.classConfig.listeners.selectionchange = function(p,records){
			if(!me.picker) return;
			
			var data = null;
			if(me.picker.checkOne){
				data = records ? records[0] : null;
				me._checkedData = data;
				me.collapse();
				if(data){
					me.fireEvent('check',me,data);
				}
			}else{
				data = records.length > 0 ? records : null;
				me._checkedData = data;
				me.fireEvent('check',me,data);
			}
		};

        var config = Ext.apply({
        	xtype: 'boundlist',
            pickerField: me,
            floating: true,
            hidden: true,
            maxHeight:300
        },me.classConfig);
        
        picker = me.picker = Ext.create(me.className,config);

        return picker;
    },
    
    /**
     * @public
     * 返回选中的数据
     * 如果为空返回null，如果单选返回record，如果多选返回records
     */
    getCheckedData:function(){
    	return this._checkedData;
    },
    /**
     * @public
     * 根据字段获取值
     * 如果是多选，就采用分隔符号连接，缺省默认是英文逗号
     * @param {Object} fieldName 数据字段
     * @param {Object} separator 分隔符号
     */
    getValueByField:function(fieldName,separator){
    	if(!fieldName){
    		alert("Shell.ux.form.field.CheckTrigger的getValue方法必须传递fieldName参数！");
    		return;
    	}
    	
    	var me = this,
			checkedData = me._checkedData;
		
		if(!checkedData) return null;
		
		//单选
		if(me.picker.checkOne){
			return checkedData.get(fieldName);
		}
		
		//多选
		var len = checkedData.length,
			arr = [];
			
		for(var i=0;i<len;i++){
			arr.push(checkedData[i].get(fieldName));
		}
		separator = separator || ",";
		return arr.join(separator);
    },
    /**
     * @public
     * 更改类属性
     * @param {Object} data
     */
	changeClassConfig:function(data){
		this.setClassConfig(data);
	},
	/**
	 * @public
	 * 设置雷属性
	 * @param {Object} data
	 */
	setClassConfig:function(data){
		var me = this;
		//if(!data) return;
		me.classConfig = Ext.apply(me.classConfig,data);
	},
	
	//以下为固定内部处理方法
    expand: function() {
        var me = this,
            bodyEl, picker, collapseIf;

        if (me.rendered && !me.isExpanded && !me.isDestroyed) {
            bodyEl = me.bodyEl;
            picker = me.getPicker();
            collapseIf = me.collapseIf;

            // show the picker and set isExpanded flag
            picker.show();
            me.isExpanded = true;
            me.alignPicker();
            bodyEl.addCls(me.openCls);

            // monitor clicking and mousewheel
            me.mon(Ext.getDoc(), {
                mousewheel: collapseIf,
                mousedown: collapseIf,
                scope: me
            });
            Ext.EventManager.onWindowResize(me.alignPicker, me);
            me.fireEvent('expand', me);
            me.onExpand();
        }
    },
    onExpand: Ext.emptyFn,
    alignPicker: function() {
        var me = this,
            picker = me.getPicker();

        if (me.isExpanded) {
            if (me.matchFieldWidth) {
                // Auto the height (it will be constrained by min and max width) unless there are no records to display.
                picker.setWidth(me.bodyEl.getWidth());
            }
            if (picker.isFloating()) {
                me.doAlign();
            }
        }
    },
    /**
     * Performs the alignment on the picker using the class defaults
     * @private
     */
    doAlign: function(){
        var me = this,
            picker = me.picker,
            aboveSfx = '-above',
            isAbove;

        me.picker.alignTo(me.inputEl, me.pickerAlign, me.pickerOffset);
        // add the {openCls}-above class if the picker was aligned above
        // the field due to hitting the bottom of the viewport
        isAbove = picker.el.getY() < me.inputEl.getY();
        me.bodyEl[isAbove ? 'addCls' : 'removeCls'](me.openCls + aboveSfx);
        picker[isAbove ? 'addCls' : 'removeCls'](picker.baseCls + aboveSfx);
    },
    
    /**
     * Collapses this field's picker dropdown.
     */
    collapse: function() {
        if (this.isExpanded && !this.isDestroyed) {
            var me = this,
                openCls = me.openCls,
                picker = me.picker,
                doc = Ext.getDoc(),
                collapseIf = me.collapseIf,
                aboveSfx = '-above';

            // hide the picker and set isExpanded flag
            picker.hide();
            me.isExpanded = false;

            // remove the openCls
            me.bodyEl.removeCls([openCls, openCls + aboveSfx]);
            picker.el.removeCls(picker.baseCls + aboveSfx);

            // remove event listeners
            doc.un('mousewheel', collapseIf, me);
            doc.un('mousedown', collapseIf, me);
            Ext.EventManager.removeResizeListener(me.alignPicker, me);
            me.fireEvent('collapse', me);
            me.onCollapse();
        }
    },
	onCollapse: Ext.emptyFn,
    
    collapseIf: function(e) {
        var me = this;
        if (!me.isDestroyed && !e.within(me.bodyEl, false, true) && !e.within(me.picker.el, false, true) && !me.isEventWithinPickerLoadMask(e)) {
            me.collapse();
        }
    },
    isEventWithinPickerLoadMask: function(e) {
        var loadMask = this.picker.loadMask;
        return loadMask ? e.within(loadMask.maskEl, false, true) || e.within(loadMask.el, false, true) : false;
    },
    
    getPicker: function() {
        var me = this;
        return me.picker || (me.picker = me.createPicker());
    },
    
    close:Ext.emptyFn
});