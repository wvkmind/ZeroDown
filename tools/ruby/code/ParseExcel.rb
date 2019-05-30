require 'simple_xlsx_reader'

def open_excel(path,path2) 
    data = []
    doc = SimpleXlsxReader.open(path)
    doc.sheets.each do |sheet|
        sheet.rows.each_with_index do |line, index|
            data << line.join("\t")
        end
        save_file(data,"#{path2}/#{ARGV[2]}_#{sheet.name}.txt")
    end
end

def save_file(datas,path)
    aFile = File.new(path, "w+")
    datas.each do |data|
        aFile.puts "#{data}\n"
    end
end

begin
    file_path   = ARGV[0]
    output_path = ARGV[1]
    data_type   = ARGV[2]
    open_excel(ARGV[0],ARGV[1])
rescue => exception
    puts exception.message
end