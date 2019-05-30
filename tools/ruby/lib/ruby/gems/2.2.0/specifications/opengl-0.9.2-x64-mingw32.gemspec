# -*- encoding: utf-8 -*-
# stub: opengl 0.9.2 x64-mingw32 lib

Gem::Specification.new do |s|
  s.name = "opengl"
  s.version = "0.9.2"
  s.platform = "x64-mingw32"

  s.required_rubygems_version = Gem::Requirement.new(">= 0") if s.respond_to? :required_rubygems_version=
  s.require_paths = ["lib"]
  s.authors = ["Eric Hodel", "Lars Kanis", "Bla\u{17e} Hrastnik", "Alain Hoang", "Jan Dvorak", "Minh Thu Vo", "James Adam"]
  s.cert_chain = ["-----BEGIN CERTIFICATE-----\nMIIDLjCCAhagAwIBAgIBAjANBgkqhkiG9w0BAQUFADA9MQ4wDAYDVQQDDAVrYW5p\nczEXMBUGCgmSJomT8ixkARkWB2NvbWNhcmQxEjAQBgoJkiaJk/IsZAEZFgJkZTAe\nFw0xNDAyMjYwOTMzMDBaFw0xNTAyMjYwOTMzMDBaMD0xDjAMBgNVBAMMBWthbmlz\nMRcwFQYKCZImiZPyLGQBGRYHY29tY2FyZDESMBAGCgmSJomT8ixkARkWAmRlMIIB\nIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEApop+rNmg35bzRugZ21VMGqI6\nHGzPLO4VHYncWn/xmgPU/ZMcZdfj6MzIaZJ/czXyt4eHpBk1r8QOV3gBXnRXEjVW\n9xi+EdVOkTV2/AVFKThcbTAQGiF/bT1n2M+B1GTybRzMg6hyhOJeGPqIhLfJEpxn\nlJi4+ENAVT4MpqHEAGB8yFoPC0GqiOHQsdHxQV3P3c2OZqG+yJey74QtwA2tLcLn\nQ53c63+VLGsOjODl1yPn/2ejyq8qWu6ahfTxiIlSar2UbwtaQGBDFdb2CXgEufXT\nL7oaPxlmj+Q2oLOfOnInd2Oxop59HoJCQPsg8f921J43NCQGA8VHK6paxIRDLQID\nAQABozkwNzAJBgNVHRMEAjAAMAsGA1UdDwQEAwIEsDAdBgNVHQ4EFgQUvgTdT7fe\nx17ugO3IOsjEJwW7KP4wDQYJKoZIhvcNAQEFBQADggEBAFmIAhRT0awqLQN9e4Uv\nZEk+jUWv4zkb+TWiKFJXlwjPyjGbZY9gVfOwAwMibYOK/t/+57ZzW3d0L12OUwvo\non84NVvYtIr1/iskJFWFkMoIquAFCdi9p68stSPMQK2XcrJJuRot29fJtropsZBa\n2cpaNd/sRYdK4oep2usdKifA1lI0hIkPb3r5nLfwG2lAqBH7WZsUICHcTgR0VEbG\nz9Ug5qQp9Uz73xC9YdGvGiuOX53LYobHAR4MWi2xxDlHI+ER8mRz0eY2FUuNu/Wj\nGrqF74zpLl7/KFdHC8VmzwZS18hvDjxeLVuVI2gIGnBInqnlqv05g/l4/1pISh5j\ndS4=\n-----END CERTIFICATE-----\n"]
  s.date = "2015-01-05"
  s.description = "An OpenGL wrapper for Ruby. opengl contains bindings for OpenGL.\n\nBe sure to check out\n{GLU}[https://github.com/larskanis/glu] and\n{GLUT}[https://github.com/larskanis/glut]\ngems."
  s.email = ["drbrain@segment7.net", "lars@greiz-reinsdorf.de", "speed.the.bboy@gmail.com", "", "", "", ""]
  s.extra_rdoc_files = ["History.rdoc", "Manifest.txt", "README.rdoc", "examples/OrangeBook/3Dlabs-License.txt"]
  s.files = ["History.rdoc", "Manifest.txt", "README.rdoc", "examples/OrangeBook/3Dlabs-License.txt"]
  s.homepage = "https://github.com/larskanis/opengl"
  s.licenses = ["MIT"]
  s.rdoc_options = ["--main", "README.rdoc"]
  s.required_ruby_version = Gem::Requirement.new(">= 1.9.2")
  s.rubygems_version = "2.4.5"
  s.summary = "An OpenGL wrapper for Ruby"

  s.installed_by_version = "2.4.5" if s.respond_to? :installed_by_version

  if s.respond_to? :specification_version then
    s.specification_version = 4

    if Gem::Version.new(Gem::VERSION) >= Gem::Version.new('1.2.0') then
      s.add_development_dependency(%q<rdoc>, ["~> 4.0"])
      s.add_development_dependency(%q<rake-compiler>, [">= 0.9.1", "~> 0.9"])
      s.add_development_dependency(%q<glu>, ["~> 8.1"])
      s.add_development_dependency(%q<glut>, ["~> 8.1"])
      s.add_development_dependency(%q<hoe>, ["~> 3.13"])
    else
      s.add_dependency(%q<rdoc>, ["~> 4.0"])
      s.add_dependency(%q<rake-compiler>, [">= 0.9.1", "~> 0.9"])
      s.add_dependency(%q<glu>, ["~> 8.1"])
      s.add_dependency(%q<glut>, ["~> 8.1"])
      s.add_dependency(%q<hoe>, ["~> 3.13"])
    end
  else
    s.add_dependency(%q<rdoc>, ["~> 4.0"])
    s.add_dependency(%q<rake-compiler>, [">= 0.9.1", "~> 0.9"])
    s.add_dependency(%q<glu>, ["~> 8.1"])
    s.add_dependency(%q<glut>, ["~> 8.1"])
    s.add_dependency(%q<hoe>, ["~> 3.13"])
  end
end
