Ext.Loader.setConfig({
	enabled:true,
	paths:{'Shell':Shell.util.Path.uiPath}
});
var panel = null;
Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	
	Shell.util.Win.begin();//屏蔽快捷键
	
	function sendkey(keychar){
		var ev = document.createEvent("KeyboardEvent");
		var keycode = keychar.charCodeAt();
		
		ev.initKeyEvent("keydown", true, true, null, 0, 0, 0, 0,keycode,keychar);
		document.dispatchEvent(ev);
	}
	
	function fireKeyEvent(el, evtType, keyCode,ctrlKey){
        var evtObj;
        
        if(document.createEvent){
            if( window.KeyEvent ) {
                evtObj = document.createEvent('KeyEvents');
                evtObj.initKeyEvent( evtType, true, true, window, false, false, false, false, keyCode, 0 );
            } else {
                evtObj = document.createEvent('UIEvents');
                evtObj.initUIEvent( evtType, true, true, window, 1 );
                delete evtObj.keyCode;
               
                if(typeof evtObj.keyCode === "undefined"){
                    Object.defineProperty(evtObj,"keyCode",{value:keyCode});
                }else{
                    evtObj.key=String.fromCharCode(keyCode);
                }
            }
			evtObj.ctrlKey = ctrlKey;
            el.dispatchEvent(evtObj);
            
        }else if(document.createEventObject){
            evtObj = document.createEventObject();
            evtObj.ctrlKey = ctrlKey;
            evtObj.keyCode=keyCode;
            el.fireEvent('on'+evtType, evtObj);
       }
    }
	
	Ext.getDoc().on("contextmenu",function(e){
		var menu = Shell.util.Win.menu || Ext.create('Ext.menu.Menu',{
			width:100,
		    margin:'0 0 10 0',
		    floating:true,
		    renderTo:Ext.getBody(),
		    items:[{
		        text:'正常显示',
		        handler:function(but,e){
		        	//e.ctrlKey = true;//按下Ctrl键
		        	//e.keyCode = 48;//按下0键
		        	fireKeyEvent(document,"keydown",48,true);
		        }
		    },{
		        text:'放大',//'老年人版',
		        handler:function(but,e){
		        	//e.ctrlKey = true;//按下Ctrl键
		        	//e.keyCode = 187;//按下+键
		        	fireKeyEvent(document,"keydown",187,true);
		        }
		    }]
		});
		menu.showAt(e.getXY());
    });   
	
	panel = Ext.create('Shell.OrderEntry.class.OrderEntryApp',{header:false});
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		padding:2,
		items:[panel]
	});
});