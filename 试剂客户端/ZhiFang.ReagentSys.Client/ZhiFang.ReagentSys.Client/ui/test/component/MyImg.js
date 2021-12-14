
Ext.define('MyImg', {
    extend: 'Ext.Component',
    alias: ['widget.myimage'],
    
    //------------------HUJIE--------------------
    //-------------------------------------------
    
    //------------------HUJIE--------------------
    text:'',//显示的文字
    maxLength:4,//文字显示的最大位数
    tooltip:'',//提示框内容
    initListeners:function(){
    	var me = this;
    	me.listeners = me.listeners ? me.listeners : {};
    	
    	var listeners = {
    		mouseover:{
    			el:'element',
    			fn:function(e,t,eOpts){
    				alert("mouseover");
    			}
    		},
    		mouseout:{
    			el:'element',
    			fn:function(e,t,eOpts){
    				alert("mouseout");
    			}
    		}
    	};
    	
    	for(var i in listeners){
    		me.listeners[i] = listeners[i];
    	}
    },
    initComponent:function(){
    	
    },
    //-------------------------------------------

    autoEl: 'img',

    /**
     * @cfg {String} src
     * The image src.
     */
    src: '',

    /**
     * @cfg {String} alt
     * The descriptive text for non-visual UI description.
     */
    alt: '',

    /**
     * @cfg {String} imgCls
     * Optional CSS classes to add to the img element.
     */
    imgCls: '',

    getElConfig: function() {
        var me = this,
            config = me.callParent(),
            img;

        // It is sometimes helpful (like in a panel header icon) to have the img wrapped
        // by a div. If our autoEl is not 'img' then we just add an img child to the el.
        if (me.autoEl == 'img') {
            img = config;
        } else {
            config.cn = [img = {
                tag: 'img',
                id: me.id + '-img'
            }];
        }

        if (me.imgCls) {
            img.cls = (img.cls ? img.cls + ' ' : '') + me.imgCls;
        }

        img.src = me.src || Ext.BLANK_IMAGE_URL;
        if (me.alt) {
            img.alt = me.alt;
        }

        return config;
    },

    onRender: function () {
        var me = this,
            el;

        me.callParent(arguments);

        el = me.el;
        me.imgEl = (me.autoEl == 'img') ? el : el.getById(me.id + '-img');
    },

    onDestroy: function () {
        Ext.destroy(this.imgEl);
        this.imgEl = null;
        this.callParent();
    },

    /**
     * Updates the {@link #src} of the image.
     * @param {String} src
     */
    setSrc: function(src) {
        var me = this,
            imgEl = me.imgEl;

        me.src = src;

        if (imgEl) {
            imgEl.dom.src = src || Ext.BLANK_IMAGE_URL;
        }
    }
});