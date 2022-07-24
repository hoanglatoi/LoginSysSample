using LogiSysSvr.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogiSysSvr
{
    public class IdentityUserInitializer
    {
        // 初期化時のロール
        public static readonly string SystemManagerRole = "SystemManager";      // システム管理権限
        public static readonly string QAManagerRole = "QAManager";      // システム管理権限

        // 初期化時のシステム管理ユーザーID
        public static readonly string StstemManageEmail = "admin@kanatec.jp";            // 最初のシステム管理ユーザーのメールアドレス
        public static readonly string StstemManagePassword = "!initialPassword01";      // 最初のシステム管理ユーザーの初期パスワード
        public static readonly string NormalUserEmail = "user@kanatec.jp";                // テスト用一般ユーザーのメールアドレス
        public static readonly string NormalUserPassword = "!User01";                   // テスト用一般ユーザーの初期パスワード

        /// <summary>
        /// ユーザーとロールの初期化
        /// 　初期のシステムユーザーあが存在しない場合のみ内容が実行される。存在する場合は何もせずに終了
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            // ユーザー管理を取得(using Microsoft.Extensions.DependencyInjectionがないとエラーになる)
            var userManager = serviceProvider.GetService<UserManager<MyIdentifyUser>>();

            // 初期のユーザーマネージャーが存在しなければロールの作成と初期システムユーザーを作成する
            var systemManager = await userManager.FindByNameAsync(StstemManageEmail);
            if (systemManager == null)
            {
                // ロール管理を取得
                var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

                // ロールの追加
                await roleManager.CreateAsync(new IdentityRole(SystemManagerRole));    // システム管理ロール
                await roleManager.CreateAsync(new IdentityRole(QAManagerRole));    // システム管理ロール

                // 初期システム管理者の作成
                // なぜか知らないが、デフォルトのログイン画面はユーザーIDではなくメールアドレスを要求し、バリデーションもメールで設定されている。
                // ところが、ログイン処理自体は「Email」ではなく「UserName」で行われるので両方に設定せざるを得ない。
                // なんでこんなことのなっているのか？　変更するにはログイン画面を変えればいい
                systemManager = new MyIdentifyUser { UserName = StstemManageEmail, Email = StstemManageEmail, EmailConfirmed = true };
                await userManager.CreateAsync(systemManager, StstemManagePassword);

                // システム管理ユーザーにシステム管理ロールを追加
                systemManager = await userManager.FindByNameAsync(StstemManageEmail);
                await userManager.AddToRoleAsync(systemManager, SystemManagerRole);
                await userManager.AddToRoleAsync(systemManager, QAManagerRole);

                // テスト用の一般ユーザー作成
                var normalUser = new MyIdentifyUser { UserName = NormalUserEmail, Email = NormalUserEmail, EmailConfirmed = true };
                await userManager.CreateAsync(normalUser, NormalUserPassword);
            }
        }
    }
}
