#SimpleDoc
Name will change, jusr R&D ing atm

- Need a way to abstract ZPL languange
- Why?
    - Octavian use case is pretty narrow. We want to print text, images and barcodes on a variety of printers
    - SimpleDoc presents an object graph of high-level components which means:
        - We can abstract DPI / Label size stuff
        - Portable
        - Can use templates to drive label gen. What i'm really trying to avoid is having to do a release just to tweak a label
        
        
##Label
- Top level component
    - Although, we might want to make a label collection/batch the top level. Can we print multiple labels ina single ZPL string?
    
    
 
 <label>
    <sections>
        <p name="some text" margin="50% 0 0 0" horizontal-align="left" font="0" h="20pt">
            <text>line one</text>
            <text>line two</text>
            <text>line three</text>
        </p>
        
        <box name="some box" margin="10pt 10pt 10pt 10pt" thickness="2pt"/>
        
        <barcode x="" y="" w="" h="">SOME_CONTENT</barcode>
    </sections>
 </label>