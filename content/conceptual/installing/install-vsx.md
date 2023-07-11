---
uid: install-vsx
level: 100
---

# Installing Metalama Tools for Visual Studio

Metalama Tools for Visual Studio is an extension that enhances your development experience by providing features such as syntax highlighting for aspect codes, CodeLens additions, and the ability to compare your original source code against the transformed code. While this extension is optional, it is highly recommended for a more comprehensive understanding of your aspect-oriented code.

> [!WARNING]
> Metalama Tools for Visual Studio require Visual Studio 2022.

1. Go to `Extensions` > `Manage Extensions`.

    ![step1](../../images/ext_manage_1.png)

2. This will open the following dialog:

    ![step2](../../images/ext_manage_2.png)

3. In the search box to the right of the prompt, type "Metalama".

    ![step3](../../images/ext_manage_3.png)

4. Click the `Download` button to begin the download process.

    ![step4](../../images/ext_manage_4.png)

5. Once the extension is downloaded, it will be ready for installation as soon as all instances of Visual Studio are closed. This requirement is highlighted at the bottom of the screen.

    ![step5](../../images/ext_manage_5.png)

6. Provide consent for the installation of the extension.

    The installation process will begin automatically when Visual Studio is closed.

    ![wizard_init](../../images/ext_manage_6.png)

    The installation wizard operates independently and will request your consent during the following stage:

    ![wizard_asking_consent](../../images/ext_manage_consent.png)

7. Click "Modify" to finalize the installation.

    To continue with the installation, click on the `Modify` button. The wizard will then proceed with the installation process.

    ![metalama_install_progress](../../images/metalama_install_progress.png)

    Upon successful installation, the wizard will display the following screen:

    ![metalama_install_done](../../images/metalama_install_done.png)

To verify the successful installation of the extension, navigate to the Extensions Manager. If the installation was successful, a green tick mark will be displayed on the top right corner of the extension icon.

![metalama_already_installed](../../images/metalama_already_installed.png)

## Installing Metalama Command-Line Tool

While the Metalama command-line tool may not be necessary during your initial interactions with Metalama, it is beneficial to be aware of its existence. For more information, refer to <xref:dotnet-tool>. We will remind you about this tool when it becomes relevant to your work.
