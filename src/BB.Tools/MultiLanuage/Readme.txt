本项目为方便多个模块中共享多语言的处理而使用。
多语言的核心思路是使用本地化语言进行开发（如中文或者英文），然后如果需要使用其他语言版本，则在运行目录的Lang/en-US或者Lang/zh-cn等目录进行其他版本的文字翻译处理。
多语言采用JSON格式文档，以字典方式键值方式进行对应，在程序启动的时候会自动加载Lang目录下的这些JSON资源。
如我们使用本地中文语言开发程序的时候，不需要可以为多语言进行特殊处理，可以按常规的添加中文名称标签和文本内容。
窗体会加载的时候，自动从界面BaseForm或者BaseDock基类进行多语言处理。