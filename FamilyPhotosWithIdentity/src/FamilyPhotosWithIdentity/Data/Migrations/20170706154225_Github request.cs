using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FamilyPhotosWithIdentity.Data.Migrations
{
    public partial class Githubrequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Config",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    content_type = table.Column<string>(nullable: true),
                    insecure_ssl = table.Column<string>(nullable: true),
                    url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Config", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Last_Response",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(nullable: true),
                    message = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Last_Response", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    avatar_url = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    events_url = table.Column<string>(nullable: true),
                    hooks_url = table.Column<string>(nullable: true),
                    issues_url = table.Column<string>(nullable: true),
                    login = table.Column<string>(nullable: true),
                    members_url = table.Column<string>(nullable: true),
                    public_members_url = table.Column<string>(nullable: true),
                    repos_url = table.Column<string>(nullable: true),
                    url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Hooks",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventsInDb = table.Column<string>(nullable: true),
                    active = table.Column<bool>(nullable: false),
                    configid = table.Column<int>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    last_responseid = table.Column<int>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    ping_url = table.Column<string>(nullable: true),
                    test_url = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true),
                    updated_at = table.Column<DateTime>(nullable: false),
                    url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hooks", x => x.id);
                    table.ForeignKey(
                        name: "FK_Hooks_Config_configid",
                        column: x => x.configid,
                        principalTable: "Config",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hooks_Last_Response_last_responseid",
                        column: x => x.last_responseid,
                        principalTable: "Last_Response",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GithubRequests",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    action = table.Column<string>(nullable: true),
                    hook_id = table.Column<int>(nullable: false),
                    hookid = table.Column<int>(nullable: true),
                    issueid = table.Column<int>(nullable: true),
                    organizationid = table.Column<int>(nullable: true),
                    repositoryid = table.Column<int>(nullable: true),
                    senderid = table.Column<int>(nullable: true),
                    zen = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GithubRequests", x => x.id);
                    table.ForeignKey(
                        name: "FK_GithubRequests_Hooks_hookid",
                        column: x => x.hookid,
                        principalTable: "Hooks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GithubRequests_Organizations_organizationid",
                        column: x => x.organizationid,
                        principalTable: "Organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    Issueid = table.Column<int>(nullable: true),
                    _default = table.Column<bool>(nullable: false),
                    color = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "GithubUsers",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    Issueid = table.Column<int>(nullable: true),
                    avatar_url = table.Column<string>(nullable: true),
                    events_url = table.Column<string>(nullable: true),
                    followers_url = table.Column<string>(nullable: true),
                    following_url = table.Column<string>(nullable: true),
                    gists_url = table.Column<string>(nullable: true),
                    gravatar_id = table.Column<string>(nullable: true),
                    html_url = table.Column<string>(nullable: true),
                    login = table.Column<string>(nullable: true),
                    organizations_url = table.Column<string>(nullable: true),
                    received_events_url = table.Column<string>(nullable: true),
                    repos_url = table.Column<string>(nullable: true),
                    site_admin = table.Column<bool>(nullable: false),
                    starred_url = table.Column<string>(nullable: true),
                    subscriptions_url = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true),
                    url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GithubUsers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Milestones",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    creatorid = table.Column<int>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    html_url = table.Column<string>(nullable: true),
                    labels_url = table.Column<string>(nullable: true),
                    number = table.Column<int>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milestones", x => x.id);
                    table.ForeignKey(
                        name: "FK_Milestones_GithubUsers_creatorid",
                        column: x => x.creatorid,
                        principalTable: "GithubUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Repositories",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    _private = table.Column<bool>(nullable: false),
                    archive_url = table.Column<string>(nullable: true),
                    assignees_url = table.Column<string>(nullable: true),
                    blobs_url = table.Column<string>(nullable: true),
                    branches_url = table.Column<string>(nullable: true),
                    clone_url = table.Column<string>(nullable: true),
                    collaborators_url = table.Column<string>(nullable: true),
                    comments_url = table.Column<string>(nullable: true),
                    commits_url = table.Column<string>(nullable: true),
                    compare_url = table.Column<string>(nullable: true),
                    contents_url = table.Column<string>(nullable: true),
                    contributors_url = table.Column<string>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    default_branch = table.Column<string>(nullable: true),
                    deployments_url = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    downloads_url = table.Column<string>(nullable: true),
                    events_url = table.Column<string>(nullable: true),
                    fork = table.Column<bool>(nullable: false),
                    forks = table.Column<int>(nullable: false),
                    forks_count = table.Column<int>(nullable: false),
                    forks_url = table.Column<string>(nullable: true),
                    full_name = table.Column<string>(nullable: true),
                    git_commits_url = table.Column<string>(nullable: true),
                    git_refs_url = table.Column<string>(nullable: true),
                    git_tags_url = table.Column<string>(nullable: true),
                    git_url = table.Column<string>(nullable: true),
                    has_downloads = table.Column<bool>(nullable: false),
                    has_issues = table.Column<bool>(nullable: false),
                    has_pages = table.Column<bool>(nullable: false),
                    has_projects = table.Column<bool>(nullable: false),
                    has_wiki = table.Column<bool>(nullable: false),
                    homepage = table.Column<string>(nullable: true),
                    hooks_url = table.Column<string>(nullable: true),
                    html_url = table.Column<string>(nullable: true),
                    issue_comment_url = table.Column<string>(nullable: true),
                    issue_events_url = table.Column<string>(nullable: true),
                    issues_url = table.Column<string>(nullable: true),
                    keys_url = table.Column<string>(nullable: true),
                    labels_url = table.Column<string>(nullable: true),
                    language = table.Column<string>(nullable: true),
                    languages_url = table.Column<string>(nullable: true),
                    merges_url = table.Column<string>(nullable: true),
                    milestones_url = table.Column<string>(nullable: true),
                    mirror_url = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    notifications_url = table.Column<string>(nullable: true),
                    open_issues = table.Column<int>(nullable: false),
                    open_issues_count = table.Column<int>(nullable: false),
                    ownerid = table.Column<int>(nullable: true),
                    pulls_url = table.Column<string>(nullable: true),
                    pushed_at = table.Column<DateTime>(nullable: false),
                    releases_url = table.Column<string>(nullable: true),
                    size = table.Column<int>(nullable: false),
                    ssh_url = table.Column<string>(nullable: true),
                    stargazers_count = table.Column<int>(nullable: false),
                    stargazers_url = table.Column<string>(nullable: true),
                    statuses_url = table.Column<string>(nullable: true),
                    subscribers_url = table.Column<string>(nullable: true),
                    subscription_url = table.Column<string>(nullable: true),
                    svn_url = table.Column<string>(nullable: true),
                    tags_url = table.Column<string>(nullable: true),
                    teams_url = table.Column<string>(nullable: true),
                    trees_url = table.Column<string>(nullable: true),
                    updated_at = table.Column<DateTime>(nullable: false),
                    url = table.Column<string>(nullable: true),
                    watchers = table.Column<int>(nullable: false),
                    watchers_count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repositories", x => x.id);
                    table.ForeignKey(
                        name: "FK_Repositories_GithubUsers_ownerid",
                        column: x => x.ownerid,
                        principalTable: "GithubUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    assigneeid = table.Column<int>(nullable: true),
                    body = table.Column<string>(nullable: true),
                    closed_at = table.Column<DateTime>(nullable: true),
                    comments = table.Column<int>(nullable: false),
                    comments_url = table.Column<string>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    events_url = table.Column<string>(nullable: true),
                    html_url = table.Column<string>(nullable: true),
                    labels_url = table.Column<string>(nullable: true),
                    locked = table.Column<bool>(nullable: false),
                    milestoneid = table.Column<int>(nullable: true),
                    number = table.Column<int>(nullable: false),
                    repository_url = table.Column<string>(nullable: true),
                    state = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true),
                    updated_at = table.Column<DateTime>(nullable: false),
                    url = table.Column<string>(nullable: true),
                    userid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.id);
                    table.ForeignKey(
                        name: "FK_Issues_GithubUsers_assigneeid",
                        column: x => x.assigneeid,
                        principalTable: "GithubUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issues_Milestones_milestoneid",
                        column: x => x.milestoneid,
                        principalTable: "Milestones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issues_GithubUsers_userid",
                        column: x => x.userid,
                        principalTable: "GithubUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GithubRequests_hookid",
                table: "GithubRequests",
                column: "hookid");

            migrationBuilder.CreateIndex(
                name: "IX_GithubRequests_issueid",
                table: "GithubRequests",
                column: "issueid");

            migrationBuilder.CreateIndex(
                name: "IX_GithubRequests_organizationid",
                table: "GithubRequests",
                column: "organizationid");

            migrationBuilder.CreateIndex(
                name: "IX_GithubRequests_repositoryid",
                table: "GithubRequests",
                column: "repositoryid");

            migrationBuilder.CreateIndex(
                name: "IX_GithubRequests_senderid",
                table: "GithubRequests",
                column: "senderid");

            migrationBuilder.CreateIndex(
                name: "IX_Hooks_configid",
                table: "Hooks",
                column: "configid");

            migrationBuilder.CreateIndex(
                name: "IX_Hooks_last_responseid",
                table: "Hooks",
                column: "last_responseid");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_assigneeid",
                table: "Issues",
                column: "assigneeid");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_milestoneid",
                table: "Issues",
                column: "milestoneid");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_userid",
                table: "Issues",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_Labels_Issueid",
                table: "Labels",
                column: "Issueid");

            migrationBuilder.CreateIndex(
                name: "IX_Milestones_creatorid",
                table: "Milestones",
                column: "creatorid");

            migrationBuilder.CreateIndex(
                name: "IX_Repositories_ownerid",
                table: "Repositories",
                column: "ownerid");

            migrationBuilder.CreateIndex(
                name: "IX_GithubUsers_Issueid",
                table: "GithubUsers",
                column: "Issueid");

            migrationBuilder.AddForeignKey(
                name: "FK_GithubRequests_Issues_issueid",
                table: "GithubRequests",
                column: "issueid",
                principalTable: "Issues",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GithubRequests_Repositories_repositoryid",
                table: "GithubRequests",
                column: "repositoryid",
                principalTable: "Repositories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GithubRequests_GithubUsers_senderid",
                table: "GithubRequests",
                column: "senderid",
                principalTable: "GithubUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Labels_Issues_Issueid",
                table: "Labels",
                column: "Issueid",
                principalTable: "Issues",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GithubUsers_Issues_Issueid",
                table: "GithubUsers",
                column: "Issueid",
                principalTable: "Issues",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GithubUsers_Issues_Issueid",
                table: "GithubUsers");

            migrationBuilder.DropTable(
                name: "GithubRequests");

            migrationBuilder.DropTable(
                name: "Labels");

            migrationBuilder.DropTable(
                name: "Hooks");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Repositories");

            migrationBuilder.DropTable(
                name: "Config");

            migrationBuilder.DropTable(
                name: "Last_Response");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "Milestones");

            migrationBuilder.DropTable(
                name: "GithubUsers");
        }
    }
}
