# XPM

**XPM** or **XAMPP Project Manager** is a simple utility to manage your XAMPP-related projects, with support for Laravel.

![](https://github.com/ldemafelix/xpm/raw/master/screenshot.jpg)

# Pre-requisites

> **WARNING:** XPM assumes full control of your HOSTS file. It will delete existing records. Proceed with caution.

* .NET Framework 4.6.1
* XAMPP
  * Apache **must** be installed as a service
  * Apache's service name must be the default: **Apache2.4**
* Composer (optional, required if Laravel will be used)
* Laravel's cmd utility (optional, recommended)

# Setup

[Download the latest release](https://github.com/ldemafelix/xpm/releases/download/v1.3.0/setup.exe) and run it to install XPM.

# Usage

> **NOTE:** XPM automatically restarts the Apache service for its operations. You do not need to restart the service yourself.

The first time you open XPM, it'll modify your XAMPP configuration, specifically `apache\conf\extra\httpd-vhosts.conf`. It'll also add a `vhosts` folder inside the `apache\conf` folder.

## Creating a Project

Type the name of your project in the text field. Make sure that your project name is **lowercase** and **alphanumeric**. If you will use Laravel, make sure **Composer** and the **Laravel cmd utility** is installed and configured. Check **Use Laravel Template** so that XPM will know the correct `DocumentRoot` to use. Optionally, you can check `Run 'laravel new'` so XPM can call the Laravel cmd utility to build your new project.

Checking **Create DNS Hosts Entry** creates an entry in the Windows HOSTS file. This is useful if you have no DNS proxy. Upon creation, your project can be accessed at `http://<your-project>.local/`.

## Deleting a Project

Select the project's configuration file and click **Delete**. It will also ask you if you wish to keep the project folder or delete it.

# License

XPM is licensed under the MIT Open Source License.

```
Copyright 2017-2018 Liam Demafelix

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
```
