# Get_StockHistory_JsonToText_Daily
This application's functions are the following ,

	1 - Gets historical daily stock data specified in the shareList variable by using the https://financialmodelingprep.com API. 
		Current list contains AAPL, GOOG and MSFT for APPLE, GOOGLE and MICROSOFT stocks respectively.

	2 - Writes the json formatted data in "sticker.json" file where sticker is the name of the stock such as AAPL for APPLE

	3 - Deserialize stock json data and extracts the most important price and volume data. These are "<date>,<open>,<high>,
		<low>,<close>,<vol> "

	4 - Write the extracted stock data to the "sticker.txt" file line by line, one line for each day of stock data. This text
		file can be loaded to financial applications for further processing.
